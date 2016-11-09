using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LineChar
{
    /*
          在．net中，微软给我们提供了画图类（system.drawing.imaging），在该类中画图的基本功能都有。比如：直线、折线、矩形、多边形、椭圆形、扇形、曲线等等，因此一般的图形都可以直接通过代码画出来。接下来介绍一些画图函数：
          Bitmap bmap=new Bitmap(500,500)　/定义图像大小；
          bmap.Save(stream,imagecodecinfo) /将图像保存到指定的输出流；
          Graphics gph /定义或创建gdi绘图对像；
          PointF left_down　/定义二维平面中x,y坐标；
          DrawString(string,font,brush,ponitf) /用指定的brush和font对像在指定的矩形或点绘制指定的字符串；
          DrawLine(pen,ponit,ponit) /用指定的笔(pen)对像绘制指定两点之间直线；
          DrawPolygon(pen,ponit[]) /用指定的笔(pen)对像绘制指定多边形，比如三角形，四边形等等；
          FillPolygon(brush,ponit[]) /用指定的刷子(brush)对像填充指定的多边形；
          DrawEllipse(pen,x,y,width,height) /用指定的笔绘制一个边框定义的椭圆；
          FillEllipse(brush,x,y,width,height) /用指定的刷子填充一个边框定义的椭圆；
          DrawRectangle(pen,x,y,width,height) /用指定的笔绘制一个指定坐标点、宽度、高度的矩形；
          DrawPie(pen,x,y,width,height,startangle,sweepangle) /用指定的笔绘制一个指定坐标点、宽度、高度以及两条射线组成的扇形；

          下面的例子可以画出一个折线图，代码如下：
       */

    public class LineCharLib
    {
        const int DATASIZE = 50;
        //画图初始化
        Bitmap bmap;
        Graphics gph;

        int grap_width = 500;       //画布宽
        int grap_height = 500;      //画布高


        int MinY = 0;       //Y轴起始值，可以是负数
        float scaleY = 0;   //Y轴刻度最小单位  可以是0.1
        int spaceY = 20;     //Y轴刻度间距 一般固定为20 框体拉长显示更多范围
        int spaceX = 0;     //X轴刻度间距 一般根据框体大小 框体拉长则拉长间隔
        int maxCount = 0;   //最大刻度个数
        float step = 0;     //数值对应的step个像素点
        public int dataArrayIndex = 0;
        int channelNum;

        float curMaxValue = 0;//当前最大值
        float curMinValue = 0;//当前最小值
        public float hisMaxValue = 0;//历史最大值
        public float hisMinValue = 0;//历史最小值

        float[,] dataArray;
        Brush[] dataColor = {
Brushes.BlueViolet,
Brushes.Brown,
Brushes.BurlyWood,
//Brushes.CadetBlue,
Brushes.Chartreuse,
//Brushes.Chocolate,
//Brushes.Coral,
Brushes.CornflowerBlue,
Brushes.Crimson,
//Brushes.Cyan,
Brushes.DarkBlue,
Brushes.DarkCyan,
Brushes.DarkGoldenrod,
Brushes.DarkGray,
Brushes.DarkOrange,
Brushes.DarkRed,
Brushes.DarkSalmon,
Brushes.DarkSeaGreen,
Brushes.DarkTurquoise,
Brushes.DarkViolet,
Brushes.DeepPink,
Brushes.DeepSkyBlue,
Brushes.DimGray,
Brushes.DodgerBlue,
Brushes.Firebrick,
Brushes.FloralWhite,
Brushes.ForestGreen,
Brushes.Fuchsia,
Brushes.Gainsboro,
Brushes.Gold,
Brushes.Goldenrod,
Brushes.Gray,
Brushes.Green,
Brushes.GreenYellow,
Brushes.Honeydew,
Brushes.HotPink,
Brushes.IndianRed,
Brushes.Indigo,
Brushes.Ivory,
Brushes.Khaki,
Brushes.Lavender,
Brushes.LavenderBlush,
Brushes.LawnGreen,
Brushes.LemonChiffon,
Brushes.LightBlue,
Brushes.LightCoral,
Brushes.LightCyan,
Brushes.LightGoldenrodYellow,
Brushes.LightGray,
Brushes.LightGreen,
Brushes.LightPink,
Brushes.LightSalmon,
Brushes.LightSeaGreen,
Brushes.LightSkyBlue,
Brushes.LightSlateGray,
Brushes.LightSteelBlue,
Brushes.LightYellow,
Brushes.Lime,
Brushes.LimeGreen,
Brushes.Linen,
Brushes.Magenta,
Brushes.Maroon,
Brushes.MediumAquamarine,
Brushes.MediumBlue,
Brushes.MediumOrchid,
Brushes.MediumPurple,
Brushes.MediumSeaGreen,
Brushes.MediumSlateBlue,
Brushes.MediumSpringGreen,
Brushes.MediumTurquoise,
Brushes.MediumVioletRed,
Brushes.MidnightBlue,
Brushes.MintCream,
Brushes.MistyRose,
Brushes.Moccasin,
Brushes.NavajoWhite,
Brushes.Navy,
Brushes.OldLace,
Brushes.Olive,
Brushes.OliveDrab,
Brushes.Orange,
Brushes.OrangeRed,
Brushes.Orchid,
Brushes.PaleGoldenrod,
Brushes.PaleGreen,
Brushes.PaleTurquoise,
Brushes.PaleVioletRed,
Brushes.PapayaWhip,
Brushes.PeachPuff,
Brushes.Peru,
Brushes.Pink,
Brushes.Plum,
Brushes.PowderBlue,
Brushes.Purple,
Brushes.Red,
Brushes.RosyBrown,
Brushes.RoyalBlue,
Brushes.SaddleBrown,
Brushes.Salmon,
Brushes.SandyBrown,
Brushes.SeaGreen,
Brushes.SeaShell,
Brushes.Sienna,
Brushes.Silver,
Brushes.SkyBlue,
Brushes.SlateBlue,
Brushes.SlateGray,
Brushes.Snow,
Brushes.SpringGreen,
Brushes.SteelBlue,
Brushes.Tan,
Brushes.Teal,
Brushes.Thistle,
Brushes.Tomato,
Brushes.Transparent,
Brushes.Turquoise,
Brushes.Violet,
Brushes.Wheat,
Brushes.White,
Brushes.WhiteSmoke,
Brushes.Yellow,
Brushes.YellowGreen };

        int LineCharInit(Form1 form){
            return 0;
        }

        public void grap_init(int _channel) {
           
        }

        public void grap_update(int width, int height, int minY, float _scaleY, int _channel)
        {
            grap_width = width;
            grap_height = height;
            MinY = minY;
            scaleY = _scaleY;
            spaceX = (grap_width - 100) / DATASIZE;
           
            step = spaceY / scaleY;
            bmap = new Bitmap(grap_width, grap_height);
            gph = Graphics.FromImage(bmap);
            maxCount = (grap_height - 100) / spaceY;
            channelNum = _channel;
            dataArray = new float[channelNum, DATASIZE];//数据
        }

        public void put_data(float data,int channel) {
            if (dataArrayIndex == 0)
            {
                 hisMaxValue = 0;
                 hisMinValue = 9999999;
            }

            if (dataArrayIndex < DATASIZE)
            {
                dataArray[channel,dataArrayIndex] = data;
            }
            else
            {
               
                for (int i = 0; i < DATASIZE - 1;i++ )
                {
                    dataArray[channel, i] = dataArray[channel,i + 1];
                }
                dataArray[channel,DATASIZE - 1] = data;
             }

            if (channel == channelNum - 1)
            {
                if (dataArrayIndex < DATASIZE)
                    dataArrayIndex++;
                curMaxValue = 0;
                curMinValue = 9999999;
                for (int j = 0; j < channelNum; j++)
                {
                    for (int i = 0; i < dataArrayIndex - 1; i++)
                    {
                        if (dataArray[j, i] > curMaxValue)
                            curMaxValue = dataArray[j, i];
                        if (dataArray[j, i] < curMinValue)
                            curMinValue = dataArray[j, i];
                    }
                }

                if (curMaxValue > hisMaxValue)
                    hisMaxValue = curMaxValue;
                if (curMinValue < hisMinValue)
                    hisMinValue = curMinValue;

            }
        }
        public Bitmap flush()
        {


            gph.Clear(Color.LightGray);

            PointF left_down = new PointF(50, grap_height - 40);//左下
            PointF left_up = new PointF(50, 50);//左上
            PointF right_down = new PointF(grap_width - 40, grap_height - 40);//右下
            PointF right_up = new PointF(grap_width - 40, 40);//右上

            /* PointF sou = new PointF(40,400);//右下

             gph.DrawEllipse(Pens.Black,sou.X, sou.Y, 3, 3);
             gph.FillEllipse(new SolidBrush(Color.Black), sou.X, sou.Y, 3, 3);
             pictureBox1.Image = bmap;
             return;*/

            //gph.DrawString("某工厂某产品月生产量图表", new Font("宋体", 14), Brushes.Black, new PointF(left_down.X + 60, left_down.X));//图表标题

            //画x轴
            PointF[] xpt = new PointF[3] { new PointF(right_down.X + 15, right_down.Y), new PointF(right_down.X, right_down.Y - 8), new PointF(right_down.X, right_down.Y + 8) };//x轴三角形
            gph.DrawLine(Pens.Black, left_down.X, left_down.Y, right_down.X, right_down.Y);
            gph.DrawPolygon(Pens.Black, xpt);
            gph.FillPolygon(new SolidBrush(Color.Black), xpt);

            //gph.DrawString("月份", new Font("宋体", 12), Brushes.Black, new PointF(left_down.Y + 10, left_down.Y + 10));
            //画y轴 
            PointF[] ypt = new PointF[3] { new PointF(left_up.X, left_up.Y - 15), new PointF(left_up.X - 8, left_up.Y), new PointF(left_up.X + 8, left_up.Y) };//y轴三角形            
            gph.DrawLine(Pens.Black, left_down.X, left_down.Y, left_down.X, left_down.X);
            gph.DrawPolygon(Pens.Black, ypt);
            gph.FillPolygon(new SolidBrush(Color.Black), ypt);
            //gph.DrawString("单位(万)", new Font("宋体", 12), Brushes.Black, new PointF(0, 7));

            for (int i = 1; i <= maxCount; i++)
            {
                //画y轴刻度
                gph.DrawString((MinY + i * scaleY).ToString(), new Font("宋体", 11), Brushes.Black, new PointF(left_down.X - 40, left_down.Y - i * spaceY - 6));
                gph.DrawLine(Pens.Olive, left_down.X - 3, left_down.Y - i * spaceY, grap_width - 40, left_down.Y - i * spaceY);


                //画x轴项目
                // gph.DrawString(month[i - 1].Substring(0, 1), new Font("宋体", 11), Brushes.Black, new PointF(left_down.X + i * 30 - 5, left_down.Y + 5));
                // gph.DrawString(month[i - 1].Substring(1, 1), new Font("宋体", 11), Brushes.Black, new PointF(left_down.X + i * 30 - 5, left_down.Y + 20));
                // if (month[i - 1].Length > 2) gph.DrawString(month[i - 1].Substring(2, 1), new Font("宋体", 11), Brushes.Black, new PointF(left_down.X + i * 30 - 5, left_down.Y + 35));  
            }
            
            //画当前最大值、最小值   历史最大值最小值
            gph.DrawLine(Pens.Green, left_down.X, left_down.Y - (curMaxValue - MinY) * step, right_down.X, right_down.Y - (curMaxValue - MinY) * step);
            gph.DrawLine(Pens.Green, left_down.X, left_down.Y - (curMinValue - MinY) * step, right_down.X, right_down.Y - (curMinValue - MinY) * step);
           
            gph.DrawLine(Pens.Blue, left_down.X, left_down.Y - (hisMaxValue - MinY) * step, right_down.X, right_down.Y - (hisMaxValue - MinY) * step);
            gph.DrawString("最大值：" + hisMaxValue.ToString(), new Font("宋体", 12), Brushes.Black, new PointF(left_down.X + 40, right_down.Y - (hisMaxValue - MinY) * step - 20));
            gph.DrawLine(Pens.Blue, left_down.X, left_down.Y - (hisMinValue - MinY) * step, right_down.X, right_down.Y - (hisMinValue - MinY) * step);
            gph.DrawString("最小值：" + hisMinValue.ToString(), new Font("宋体", 12), Brushes.Black, new PointF(left_down.X + 40, right_down.Y - (hisMinValue - MinY) * step + 10));



            PointF show_color = new PointF(0,10);//左上
            for (int j = 0; j < channelNum; j++)
            {//show_color.X = show_color.X + (j * 100);
                show_color.X =  (j * 100)+10;
                gph.DrawString("通道" + (j + 1).ToString() + ":", new Font("宋体", 10), Brushes.Black,show_color);
                gph.DrawLine(new Pen(dataColor[j], 3), show_color.X + 44, show_color.Y + 7, show_color.X + 80, show_color.Y + 7);
                for (int i = 1; i < dataArrayIndex; i++)
                {
                    //画点
                    //PointF cur = new PointF(left_down.X + i * spaceX - 1.5f, left_down.Y - (dataArray[j,(i - 1)] - MinY) * step - 1.5f);//当前值所处坐标
                    PointF cur = new PointF(left_down.X + i * spaceX, left_down.Y - (dataArray[j, (i - 1)] - MinY) * step);//当前值所处坐标
                    //gph.DrawEllipse(new Pen(dataColor[j], 3), cur.X, cur.Y, 1, 1);
                   // gph.FillEllipse(new SolidBrush(Color.Black), cur.X, cur.Y, 3, 3);
                    //画数值
                    //gph.DrawString(d[i - 1].ToString(), new Font("宋体", 11), Brushes.Black, new PointF(left_down.X + i * 30, left_down.Y - d[i - 1] * 3));
                    //画折线
                    if (i > 1)
                    {
                        PointF last = new PointF(left_down.X + (i - 1) * spaceX, left_down.Y - (dataArray[j,(i - 2)] - MinY) * step);//当前值所处坐标
                        gph.DrawLine(new Pen(dataColor[j], 3), last.X, last.Y, cur.X+1, cur.Y);
                    }
                }
            }
            //保存输出图片
            //bmap.Save(Response.OutputStream, ImageFormat.Gif);
            return bmap;

        }
    }
}
