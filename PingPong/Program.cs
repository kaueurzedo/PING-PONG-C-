using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Media;

namespace PingPong
{
    class Program : GameWindow
    {
        int xDaBola = 0;
        int yDaBola = 0;
        int tamanhoDaBola = 30;
        int velocidadeDaBolaEmx = 5;
        int velocidadeDaBolaEmy = 5;

        int yDoJogador1 = 0;
        int yDoJogador2 = 0;

        int placarDoJogador1 = 0;
        int placarDoJogador2 = 0;




        int xDoJogador1 ()
        {
            return -ClientSize.Width / 2 + LarguraDosJogadores() / 2;
        }

        int xDoJogador2()
        {
            return ClientSize.Width / 2 - LarguraDosJogadores() / 2;
        }

        int LarguraDosJogadores()
        {
            return  tamanhoDaBola;
        }
        int AlturaDosJogadores()
        {
            return 3 * tamanhoDaBola;
        }

     
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            xDaBola = xDaBola + velocidadeDaBolaEmx;
            yDaBola = yDaBola + velocidadeDaBolaEmy;

            if (xDaBola + tamanhoDaBola / 2 > xDoJogador2() - LarguraDosJogadores() /2 
                && yDaBola - tamanhoDaBola / 2 < yDoJogador2 + AlturaDosJogadores() / 2
                && yDaBola + tamanhoDaBola / 2 > yDoJogador2 - AlturaDosJogadores() /2) 
            {
                velocidadeDaBolaEmx = -velocidadeDaBolaEmx;
            }

            if (xDaBola - tamanhoDaBola / 2 < xDoJogador1 () + LarguraDosJogadores() / 2
                && yDaBola - tamanhoDaBola / 2 < yDoJogador1 + AlturaDosJogadores() / 2
                && yDaBola + tamanhoDaBola / 2 > yDoJogador1 - AlturaDosJogadores() / 2)
            {
                velocidadeDaBolaEmx = -velocidadeDaBolaEmx;
            }
            if (xDaBola + tamanhoDaBola / 2 < -ClientSize.Width / 2)
            {
                placarDoJogador2++;
                xDaBola = 0;
                yDaBola = 0;
            }

            if (xDaBola - tamanhoDaBola / 2 > ClientSize.Width / 2)
            {
                placarDoJogador1++;
                xDaBola = 0;
                yDaBola = 0;
            }
            if (yDaBola + tamanhoDaBola / 2 > ClientSize.Height / 2)
            {
                velocidadeDaBolaEmy = -velocidadeDaBolaEmy;
            }

            if (yDaBola - tamanhoDaBola / 2 < -ClientSize.Height / 2)
            {
                velocidadeDaBolaEmy = -velocidadeDaBolaEmy;
            }
            if (xDaBola < -ClientSize.Width / 2 || xDaBola > ClientSize.Width / 2)
            {
                xDaBola = 0;
                yDaBola = 0;
            }

            if (Keyboard.GetState().IsKeyDown (Key.W))
            {
                yDoJogador1 = yDoJogador1 + 5;
            }
            if (Keyboard.GetState().IsKeyDown(Key.S))
            {
                yDoJogador1 = yDoJogador1 - 5;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Up))
            {
                yDoJogador2 = yDoJogador2 + 5;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Down))
            {
                yDoJogador2 = yDoJogador2 - 5;
            }

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
             GL.Clear(ClearBufferMask.ColorBufferBit);
           

            DesenharRetangulo(xDaBola, yDaBola, tamanhoDaBola,tamanhoDaBola, 1.0f, 1.0f, 0.0f);
            DesenharRetangulo(xDoJogador1(), yDoJogador1,LarguraDosJogadores(),AlturaDosJogadores(), 1.0f, 0.0f, 0.0f);
            DesenharRetangulo(xDoJogador2(), yDoJogador2, LarguraDosJogadores(), AlturaDosJogadores(), 0.0f, 0.0f, 1.0f);
           


            SwapBuffers();

            

          
        }
        void DesenharRetangulo(int x,int y, int largura, int altura, float r, float g,float b)
        {
            GL.Color3(r, g, b);

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, 0.5f * altura +  y);
            GL.Vertex2(-0.5f * largura + x, 0.5f * altura + y);
            GL.End();

        }

        
        static void Main()
        {
            new Program().Run();    
        }
    }
}
