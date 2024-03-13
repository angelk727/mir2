using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Client.MirControls;
using Client.MirScenes;
using SlimDX;
using SlimDX.Direct3D9;
using Blend = SlimDX.Direct3D9.Blend;

namespace Client.MirGraphics
{
    class DXManager
    //用于管理游戏中与 Direct3D 相关的各种资源，包括设备、纹理、表面、渲染参数和像素着色器等
    {
        public static List<MImage> TextureList = new List<MImage>();
        public static List<MirControl> ControlList = new List<MirControl>();

        public static Device Device;
        public static Sprite Sprite;
        public static Line Line;

        public static Surface CurrentSurface;
        public static Surface MainSurface;
        public static PresentParameters Parameters;
        public static bool DeviceLost;
        public static float Opacity = 1F;
        public static bool Blending;
        public static float BlendingRate;
        public static BlendMode BlendingMode;


        public static Texture RadarTexture;
        public static List<Texture> Lights = new List<Texture>();
        public static Texture PoisonDotBackground;

        public static Texture FloorTexture, LightTexture;
        public static Surface FloorSurface, LightSurface;

        public static PixelShader GrayScalePixelShader;
        public static PixelShader NormalPixelShader;
        public static PixelShader MagicPixelShader;

        public static bool GrayScale;

        public static Point[] LightSizes =
        {
            new Point(125,95),
            new Point(205,156),
            new Point(285,217),
            new Point(365,277),
            new Point(445,338),
            new Point(525,399),
            new Point(605,460),
            new Point(685,521),
            new Point(765,581),
            new Point(845,642),
            new Point(925,703)
        };

        public static void Create()
        //游戏启动时初始化并创建了 Direct3D 设备，配置了渲染参数，并加载了游戏所需的纹理和像素着色器
        {
            Parameters = new PresentParameters
            {
                BackBufferFormat = Format.X8R8G8B8,
                PresentFlags = PresentFlags.LockableBackBuffer,
                BackBufferWidth = Settings.ScreenWidth,
                BackBufferHeight = Settings.ScreenHeight,
                SwapEffect = SwapEffect.Discard,
                PresentationInterval = Settings.FPSCap ? PresentInterval.One : PresentInterval.Immediate,
                Windowed = !Settings.FullScreen,
            };


            Direct3D d3d = new Direct3D();

            Capabilities devCaps = d3d.GetDeviceCaps(0, DeviceType.Hardware);
            DeviceType devType = DeviceType.Reference;
            CreateFlags devFlags = CreateFlags.HardwareVertexProcessing;

            if (devCaps.VertexShaderVersion.Major >= 2 && devCaps.PixelShaderVersion.Major >= 2)
                devType = DeviceType.Hardware;

            if ((devCaps.DeviceCaps & DeviceCaps.HWTransformAndLight) != 0)
                devFlags = CreateFlags.HardwareVertexProcessing;


            if ((devCaps.DeviceCaps & DeviceCaps.PureDevice) != 0)
                devFlags |= CreateFlags.PureDevice;


            Device = new Device(d3d, d3d.Adapters.DefaultAdapter.Adapter, devType, Program.Form.Handle, devFlags, Parameters);

            Device.SetDialogBoxMode(true);

            LoadTextures();
            LoadPixelsShaders();
        }

        private static unsafe void LoadPixelsShaders()
        //加载游戏中所需的像素着色器文件，并将它们编译为 Direct3D 的 PixelShader 对象，以备后续在渲染过程中使用
        {
            var shaderNormalPath = Settings.ShadersPath + "normal.ps";
            var shaderGrayScalePath = Settings.ShadersPath + "grayscale.ps";
            var shaderMagicPath = Settings.ShadersPath + "magic.ps";

            if (System.IO.File.Exists(shaderNormalPath))
            {
                using (var gs = ShaderBytecode.AssembleFromFile(shaderNormalPath, ShaderFlags.None))
                    NormalPixelShader = new PixelShader(Device, gs);
            }
            if (System.IO.File.Exists(shaderGrayScalePath))
            {
                using (var gs = ShaderBytecode.AssembleFromFile(shaderGrayScalePath, ShaderFlags.None))
                    GrayScalePixelShader = new PixelShader(Device, gs);
            }
            if (System.IO.File.Exists(shaderMagicPath))
            {
                using (var gs = ShaderBytecode.AssembleFromFile(shaderMagicPath, ShaderFlags.None))
                    MagicPixelShader = new PixelShader(Device, gs);
            }
        }

        private static unsafe void LoadTextures()
        //初始化游戏所需的 Sprite 对象、Line 对象以及一些纹理资源，其中包括雷达纹理和毒点背景纹理，并创建游戏中的光照效果
        {
            Sprite = new Sprite(Device);
            Line = new Line(Device) { Width = 1F };

            MainSurface = Device.GetBackBuffer(0, 0);
            CurrentSurface = MainSurface;
            Device.SetRenderTarget(0, MainSurface);

            if (RadarTexture == null || RadarTexture.Disposed)
            {
                RadarTexture = new Texture(Device, 2, 2, 1, Usage.None, Format.A8R8G8B8, Pool.Managed);

                DataRectangle stream = RadarTexture.LockRectangle(0, LockFlags.Discard);
                using (Bitmap image = new Bitmap(2, 2, 8, PixelFormat.Format32bppArgb, stream.Data.DataPointer))
                using (Graphics graphics = Graphics.FromImage(image))
                    graphics.Clear(Color.White);
                RadarTexture.UnlockRectangle(0);
            }
            if (PoisonDotBackground == null || PoisonDotBackground.Disposed)
            {
                PoisonDotBackground = new Texture(Device, 5, 5, 1, Usage.None, Format.A8R8G8B8, Pool.Managed);

                DataRectangle stream = PoisonDotBackground.LockRectangle(0, LockFlags.Discard);
                using (Bitmap image = new Bitmap(5, 5, 20, PixelFormat.Format32bppArgb, stream.Data.DataPointer))
                using (Graphics graphics = Graphics.FromImage(image))
                    graphics.Clear(Color.White);
                PoisonDotBackground.UnlockRectangle(0);
            }
            CreateLights();
        }

        private unsafe static void CreateLights()
        //清除现有的光照效果并创建新的光照贴图，以便在游戏中模拟光照效果
        {

            for (int i = Lights.Count - 1; i >= 0; i--)
                Lights[i].Dispose();

            Lights.Clear();

            for (int i = 1; i < LightSizes.Length; i++)
            {
                // int width = 125 + (57 *i);
                //int height = 110 + (57 * i);
                int width = LightSizes[i].X;
                int height = LightSizes[i].Y;

                Texture light = new Texture(Device, width, height, 1, Usage.None, Format.A8R8G8B8, Pool.Managed);

                DataRectangle stream = light.LockRectangle(0, LockFlags.Discard);
                using (Bitmap image = new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, stream.Data.DataPointer))
                {
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        using (GraphicsPath path = new GraphicsPath())
                        {
                            //path.AddEllipse(new Rectangle(0, 0, width, height));
                            //using (PathGradientBrush brush = new PathGradientBrush(path))
                            //{
                            //    graphics.Clear(Color.FromArgb(0, 0, 0, 0));
                            //    brush.SurroundColors = new[] { Color.FromArgb(0, 255, 255, 255) };
                            //    brush.CenterColor = Color.FromArgb(255, 255, 255, 255);
                            //    graphics.FillPath(brush, path);
                            //    graphics.Save();
                            //}

                            path.AddEllipse(new Rectangle(0, 0, width, height));
                            using (PathGradientBrush brush = new PathGradientBrush(path))
                            {
                                Color[] blendColours = { Color.White,
                                                     Color.FromArgb(255,210,210,210),
                                                     Color.FromArgb(255,160,160,160),
                                                     Color.FromArgb(255,70,70,70),
                                                     Color.FromArgb(255,40,40,40),
                                                     Color.FromArgb(0,0,0,0)};

                                float[] radiusPositions = { 0f, .20f, .40f, .60f, .80f, 1.0f };

                                ColorBlend colourBlend = new ColorBlend();
                                colourBlend.Colors = blendColours;
                                colourBlend.Positions = radiusPositions;

                                graphics.Clear(Color.FromArgb(0, 0, 0, 0));
                                brush.InterpolationColors = colourBlend;
                                brush.SurroundColors = blendColours;
                                brush.CenterColor = Color.White;
                                graphics.FillPath(brush, path);
                                graphics.Save();
                            }
                        }
                    }
                }

                light.UnlockRectangle(0);
                //light.Disposing += (o, e) => Lights.Remove(light);
                Lights.Add(light);
            }
        }

        public static void SetSurface(Surface surface)
        //将渲染目标切换到指定的表面，以便后续的绘制操作将在该表面上进行渲染
        {
            if (CurrentSurface == surface)
                return;

            Sprite.Flush();
            CurrentSurface = surface;
            Device.SetRenderTarget(0, surface);
        }
        public static void SetGrayscale(bool value)
        //根据传入的布尔值来启用或禁用灰度像素着色器，并确保绘图设备的状态与指定的值保持一致
        {
            GrayScale = value;

            if (value == true)
            {
                if (Device.PixelShader == GrayScalePixelShader) return;
                Sprite.Flush();
                Device.PixelShader = GrayScalePixelShader;
            }
            else
            {
                if (Device.PixelShader == null) return;
                Sprite.Flush();
                Device.PixelShader = null;
            }
        }

        public static void DrawOpaque(Texture texture, Rectangle? sourceRect, Vector3? position, Color4 color, float opacity)
        //在屏幕上绘制指定的纹理，并根据传入的不透明度值调整绘制时的透明度
        {
            color.Alpha = opacity;
            Draw(texture, sourceRect, position, color);
        }

        public static void Draw(Texture texture, Rectangle? sourceRect, Vector3? position, Color4 color)
        //在屏幕上绘制指定的纹理，可以控制绘制的位置、大小和颜色
        {
            Sprite.Draw(texture, sourceRect, Vector3.Zero, position, color);
            CMain.DPSCounter++;
        }

        public static void AttemptReset()
        //尝试检查设备状态并在必要时重新初始化设备，以确保设备的正常工作状态
        {
            try
            {
                Result result = DXManager.Device.TestCooperativeLevel();

                if (result.Code == ResultCode.DeviceLost.Code) return;

                if (result.Code == ResultCode.DeviceNotReset.Code)
                {
                    DXManager.ResetDevice();
                    return;
                }

                if (result.Code != ResultCode.Success.Code) return;

                DXManager.DeviceLost = false;
            }
            catch
            {
            }
        }

        public static void ResetDevice()
        //作用是在窗口大小变化或全屏模式变化时，重置绘图设备，以确保正确配置绘图设备参数，并重新初始化设备
        {
            DXManager.CleanUp();
            DXManager.DeviceLost = true;

            if (DXManager.Parameters == null) return;

            Size clientSize = Program.Form.ClientSize;

            if (clientSize.Width == 0 || clientSize.Height == 0) return;

            DXManager.Parameters.Windowed = !Settings.FullScreen;
            DXManager.Parameters.BackBufferWidth = clientSize.Width;
            DXManager.Parameters.BackBufferHeight = clientSize.Height;
            DXManager.Parameters.PresentationInterval = Settings.FPSCap ? PresentInterval.Default : PresentInterval.Immediate;
            DXManager.Device.Reset(DXManager.Parameters);

            DXManager.LoadTextures();
        }

        public static void AttemptRecovery()
        //在可能出现异常的情况下恢复绘图设备的正常状态，以确保后续的绘制操作能够继续进行
        //如果其中任何一个步骤发生异常，它会捕获异常并不做任何处理，以避免程序中断
        {
            try
            {
                Sprite.End();
            }
            catch
            {
            }

            try
            {
                Device.EndScene();
            }
            catch
            {
            }

            try
            {
                MainSurface = Device.GetBackBuffer(0, 0);
                CurrentSurface = MainSurface;
                Device.SetRenderTarget(0, MainSurface);
            }
            catch
            {
            }
        }
        public static void SetOpacity(float opacity) //根据传入的透明度值设置绘制对象的透明度
        {
            if (Opacity == opacity)
                return;

            Sprite.Flush();
            Device.SetRenderState(RenderState.AlphaBlendEnable, true);
            if (opacity >= 1 || opacity < 0)
            {
                Device.SetRenderState(RenderState.SourceBlend, SlimDX.Direct3D9.Blend.SourceAlpha);
                Device.SetRenderState(RenderState.DestinationBlend, SlimDX.Direct3D9.Blend.InverseSourceAlpha);
                Device.SetRenderState(RenderState.SourceBlendAlpha, Blend.One);
                Device.SetRenderState(RenderState.BlendFactor, Color.FromArgb(255, 255, 255, 255).ToArgb());
            }
            else
            {
                Device.SetRenderState(RenderState.SourceBlend, Blend.BlendFactor);
                Device.SetRenderState(RenderState.DestinationBlend, Blend.InverseBlendFactor);
                Device.SetRenderState(RenderState.SourceBlendAlpha, Blend.SourceAlpha);
                Device.SetRenderState(RenderState.BlendFactor, Color.FromArgb((byte)(255 * opacity), (byte)(255 * opacity), (byte)(255 * opacity), (byte)(255 * opacity)).ToArgb());
            }
            Opacity = opacity;
            Sprite.Flush();
        }
        public static void SetBlend(bool value, float rate = 1F, BlendMode mode = BlendMode.NORMAL) 
            //根据传入的参数值设置不同的混合模式，并将其应用到绘制操作中。混合模式可以控制图像在绘制时的透明度和混合效果，以实现不同的视觉效果
        {
            if (value == Blending && BlendingRate == rate && BlendingMode == mode) return;

            Blending = value;
            BlendingRate = rate;
            BlendingMode = mode;

            Sprite.Flush();

            Sprite.End();

            if (Blending)
            {
                Sprite.Begin(SpriteFlags.DoNotSaveState);
                Device.SetRenderState(RenderState.AlphaBlendEnable, true);

                switch (BlendingMode)
                {
                    case BlendMode.INVLIGHT:
                        Device.SetRenderState(RenderState.BlendOperation, BlendOperation.Add);
                        Device.SetRenderState(RenderState.SourceBlend, Blend.BlendFactor);
                        Device.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceColor);
                        break;
                    default:
                        Device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
                        Device.SetRenderState(RenderState.DestinationBlend, Blend.One);
                        break;
                }

                Device.SetRenderState(RenderState.BlendFactor, Color.FromArgb((byte)(255 * BlendingRate), (byte)(255 * BlendingRate),
                                                                (byte)(255 * BlendingRate), (byte)(255 * BlendingRate)).ToArgb());
            }
            else
                Sprite.Begin(SpriteFlags.AlphaBlend);

            Device.SetRenderTarget(0, CurrentSurface);
        }

        public static void SetNormal(float blend, Color tintcolor) //在图像上应用普通效果，并指定混合模式和色调，以实现特定的视觉效果
        {
            if (Device.PixelShader == NormalPixelShader)
                return;

            Sprite.Flush();
            Device.PixelShader = NormalPixelShader;
            Device.SetPixelShaderConstant(0, new Vector4[] { new Vector4(1.0F, 1.0F, 1.0F, blend) });
            Device.SetPixelShaderConstant(1, new Vector4[] { new Vector4(tintcolor.R / 255, tintcolor.G / 255, tintcolor.B / 255, 1.0F) });
            Sprite.Flush();
        }

        public static void SetGrayscale(float blend, Color tintcolor) //在图像上应用灰度效果，并指定混合模式和色调，以实现特定的视觉效果
        {
            if (Device.PixelShader == GrayScalePixelShader)
                return;

            Sprite.Flush();
            Device.PixelShader = GrayScalePixelShader;
            Device.SetPixelShaderConstant(0, new Vector4[] { new Vector4(1.0F, 1.0F, 1.0F, blend) });
            Device.SetPixelShaderConstant(1, new Vector4[] { new Vector4(tintcolor.R / 255, tintcolor.G / 255, tintcolor.B / 255, 1.0F) });
            Sprite.Flush();
        }

        public static void SetBlendMagic(float blend, Color tintcolor) //在魔法效果中应用指定的混合模式和色调，以实现特定的视觉效果
        {
            if (Device.PixelShader == MagicPixelShader || MagicPixelShader == null)
                return;

            Sprite.Flush();
            Device.PixelShader = MagicPixelShader;
            Device.SetPixelShaderConstant(0, new Vector4[] { new Vector4(1.0F, 1.0F, 1.0F, blend) });
            Device.SetPixelShaderConstant(1, new Vector4[] { new Vector4(tintcolor.R / 255, tintcolor.G / 255, tintcolor.B / 255, 1.0F) });
            Sprite.Flush();
        }

        public static void Clean() //在一定时间间隔内清理不再使用的纹理资源，以避免内存泄漏和资源浪费
        {
            for (int i = TextureList.Count - 1; i >= 0; i--)
            {
                MImage m = TextureList[i];

                if (m == null)
                {
                    TextureList.RemoveAt(i);
                    continue;
                }

                if (CMain.Time <= m.CleanTime) continue;

                m.DisposeTexture();
            }

            for (int i = ControlList.Count - 1; i >= 0; i--)
            {
                MirControl c = ControlList[i];

                if (c == null)
                {
                    ControlList.RemoveAt(i);
                    continue;
                }

                if (CMain.Time <= c.CleanTime) continue;

                c.DisposeTexture();
            }
        }


        private static void CleanUp() //清理程序中使用的各种图形资源，以避免内存泄漏和资源浪费
        {
            if (Sprite != null)
            {
                if (!Sprite.Disposed)
                {
                    Sprite.Dispose();
                }

                Sprite = null;
            }

            if (Line != null)
            {
                if (!Line.Disposed)
                {
                    Line.Dispose();
                }

                Line = null;
            }

            if (CurrentSurface != null)
            {
                if (!CurrentSurface.Disposed)
                {
                    CurrentSurface.Dispose();
                }

                CurrentSurface = null;
            }

            if (PoisonDotBackground != null)
            {
                if (!PoisonDotBackground.Disposed)
                {
                    PoisonDotBackground.Dispose();
                }

                PoisonDotBackground = null;
            }

            if (RadarTexture != null)
            {
                if (!RadarTexture.Disposed)
                {
                    RadarTexture.Dispose();
                }

                RadarTexture = null;
            }

            if (FloorTexture != null)
            {
                if (!FloorTexture.Disposed)
                {
                    FloorTexture.Dispose();
                }

                DXManager.FloorTexture = null;
                GameScene.Scene.MapControl.FloorValid = false;

                if (DXManager.FloorSurface != null && !DXManager.FloorSurface.Disposed)
                {
                    DXManager.FloorSurface.Dispose();
                }

                DXManager.FloorSurface = null;
            }

            if (LightTexture != null)
            {
                if (!LightTexture.Disposed)
                    LightTexture.Dispose();

                DXManager.LightTexture = null;

                if (DXManager.LightSurface != null && !DXManager.LightSurface.Disposed)
                {
                    DXManager.LightSurface.Dispose();
                }

                DXManager.LightSurface = null;
            }

            if (Lights != null)
            {
                for (int i = 0; i < Lights.Count; i++)
                {
                    if (!Lights[i].Disposed)
                        Lights[i].Dispose();
                }
                Lights.Clear();
            }

            for (int i = TextureList.Count - 1; i >= 0; i--)
            {
                MImage m = TextureList[i];

                if (m == null) continue;

                m.DisposeTexture();
            }
            TextureList.Clear();


            for (int i = ControlList.Count - 1; i >= 0; i--)
            {
                MirControl c = ControlList[i];

                if (c == null) continue;

                c.DisposeTexture();
            }
            ControlList.Clear();
        }

        public static void Dispose() //释放程序中使用的各种图形资源，以避免内存泄漏和资源浪费
        {
            CleanUp();

            Device?.Direct3D?.Dispose();
            Device?.Dispose();

            NormalPixelShader?.Dispose();
            GrayScalePixelShader?.Dispose();
            MagicPixelShader?.Dispose();
        }
    }
}