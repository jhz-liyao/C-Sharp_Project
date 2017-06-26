using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

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
        ConfigVO configVO = null;
        //画图初始化
        Bitmap bmap;
        Graphics gph;

        public int spaceY = 20;     //Y轴刻度间距 一般固定为20 框体拉长显示更多范围
        public int spaceX = 20;     //X轴刻度间距 一般根据框体大小 框体拉长则拉长间隔
        int maxCountX = 0;   //最大刻度个数
        int maxCountY = 0;   //最大刻度个数

        float stepY = 0;     //数值对应的step个像素点
        float stepX = 0;     //数值对应的step个像素点
        public int dataArrayIndex = 0;
        public int dataArrayIndexMax = 0;

        public float curMaxValue = 0;//当前最大值
        public int curMaxValueIndex = 0;//当前最大值索引
        public float curMinValue = 999999;//当前最小值
        public int curMinValueIndex = 0;//当前最小值索引
        public float hisMaxValue = 0;//历史最大值
        public float hisMinValue = 999999;//历史最小值

        float[,] dataArray;
        Brush[] dataColor = { Brushes.BlueViolet, Brushes.Brown, Brushes.BurlyWood, Brushes.Chartreuse, Brushes.Crimson, Brushes.DarkBlue, Brushes.DarkCyan, Brushes.DarkGoldenrod, Brushes.DarkGray, Brushes.DarkOrange, Brushes.DarkRed, Brushes.DarkSalmon, Brushes.DarkSeaGreen, Brushes.DarkTurquoise, Brushes.DarkViolet, Brushes.DeepPink, Brushes.DeepSkyBlue, Brushes.DimGray, Brushes.DodgerBlue, Brushes.Firebrick, Brushes.FloralWhite, Brushes.ForestGreen, Brushes.Fuchsia, Brushes.Gainsboro, Brushes.Gold, Brushes.Goldenrod, Brushes.Gray, Brushes.Green, Brushes.GreenYellow, Brushes.Honeydew, Brushes.HotPink, Brushes.IndianRed, Brushes.Indigo, Brushes.Ivory, Brushes.Khaki, Brushes.Lavender, Brushes.LavenderBlush, Brushes.LawnGreen, Brushes.LemonChiffon, Brushes.LightBlue, Brushes.LightCoral, Brushes.LightCyan, Brushes.LightGoldenrodYellow, Brushes.LightGray, Brushes.LightGreen, Brushes.LightPink, Brushes.LightSalmon, Brushes.LightSeaGreen, Brushes.LightSkyBlue, Brushes.LightSlateGray, Brushes.LightSteelBlue, Brushes.LightYellow, Brushes.Lime, Brushes.LimeGreen, Brushes.Linen, Brushes.Magenta, Brushes.Maroon, Brushes.MediumAquamarine, Brushes.MediumBlue, Brushes.MediumOrchid, Brushes.MediumPurple, Brushes.MediumSeaGreen, Brushes.MediumSlateBlue, Brushes.MediumSpringGreen, Brushes.MediumTurquoise, Brushes.MediumVioletRed, Brushes.MidnightBlue, Brushes.MintCream, Brushes.MistyRose, Brushes.Moccasin, Brushes.NavajoWhite, Brushes.Navy, Brushes.OldLace, Brushes.Olive, Brushes.OliveDrab, Brushes.Orange, Brushes.OrangeRed, Brushes.Orchid, Brushes.PaleGoldenrod, Brushes.PaleGreen, Brushes.PaleTurquoise, Brushes.PaleVioletRed, Brushes.PapayaWhip, Brushes.PeachPuff, Brushes.Peru, Brushes.Pink, Brushes.Plum, Brushes.PowderBlue, Brushes.Purple, Brushes.Red, Brushes.RosyBrown, Brushes.RoyalBlue, Brushes.SaddleBrown, Brushes.Salmon, Brushes.SandyBrown, Brushes.SeaGreen, Brushes.SeaShell, Brushes.Sienna, Brushes.Silver, Brushes.SkyBlue, Brushes.SlateBlue, Brushes.SlateGray, Brushes.Snow, Brushes.SpringGreen, Brushes.SteelBlue, Brushes.Tan, Brushes.Teal, Brushes.Thistle, Brushes.Tomato, Brushes.Transparent, Brushes.Turquoise, Brushes.Violet, Brushes.Wheat, Brushes.White, Brushes.WhiteSmoke, Brushes.Yellow, Brushes.YellowGreen };

        int LineCharInit(Form1 form){
            return 0;
        }

        public void grap_init(int _channel) {
           
        }

        public void grap_update(ConfigVO _configVO)
        {
            configVO = _configVO;
            stepX = spaceX / configVO.scaleX;
            stepY = spaceY / configVO.scaleY;
            bmap = new Bitmap(configVO.grap_width, configVO.grap_height);
            gph = Graphics.FromImage(bmap);
            maxCountX = (configVO.grap_width - 100) / spaceX;
            maxCountY = (configVO.grap_height - 100) / spaceY;
            dataArray = new float[configVO.channelNum, configVO.datasize];//数据
        }

        public void put_data(float data,int channel) {
            

            if (dataArrayIndex < configVO.datasize)//缓冲区未满赋值
            {
                dataArray[channel,dataArrayIndex] = data;
            }
            else//缓冲区已满顶出
            {
                for (int i = 0; i < configVO.datasize - 1;i++ )
                {
                    dataArray[channel, i] = dataArray[channel,i + 1];
                }
                dataArray[channel,configVO.datasize - 1] = data;
             }
            if (channel == configVO.channelNum - 1)
            {
                if (dataArrayIndex < configVO.datasize)
                    dataArrayIndex++;
            }
            if (configVO.max_min_flag == true)//统计最大最小值
            {
                //if (dataArrayIndex == 0)
                //{
                //    hisMaxValue = 0;
                //    hisMinValue = 9999999;
                //}

                if (channel == configVO.channelNum - 1)
                {
                    curMaxValue = 0;
                    curMinValue = 9999999;
                    for (int j = 0; j < configVO.channelNum; j++)
                    {
                        if (!configVO.channel1 && j == 0) continue;
                        if (!configVO.channel2 && j == 1) continue;
                        if (!configVO.channel3 && j == 2) continue;
                        if (!configVO.channel4 && j == 3) continue;
                        for (int i = 0; i < dataArrayIndex - 1; i++)
                        {
                            if (dataArray[j, i] > curMaxValue)
                            {
                                curMaxValue = dataArray[j, i];
                                curMaxValueIndex = i;
                            }
                            if (dataArray[j, i] < curMinValue)
                            {
                                curMinValue = dataArray[j, i];
                                curMinValueIndex = i;
                            }
                        }
                    }

                    if (curMaxValue > hisMaxValue)
                        hisMaxValue = curMaxValue;
                    if (curMinValue < hisMinValue)
                        hisMinValue = curMinValue;

                }
            }
        }

        public void clean_data() {
            dataArrayIndexMax = dataArrayIndex;
            dataArrayIndex = 0;
            curMaxValue = 0;//当前最大值
            curMinValue = 0;//当前最小值
            hisMaxValue = 0;//当前最大值
            hisMinValue = 0;//当前最小值
        }
        public Bitmap flush()
        {
            gph.Clear(Color.LightGray);

            PointF left_down = new PointF(50, configVO.grap_height - 40);//左下
            PointF left_up = new PointF(50, 50);//左上
            PointF right_down = new PointF(configVO.grap_width - 40, configVO.grap_height - 40);//右下
            PointF right_up = new PointF(configVO.grap_width - 40, 40);//右上

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

            Pen virtualLinePen = new Pen(Color.Black, 1);
            virtualLinePen.DashStyle = DashStyle.Custom;
            virtualLinePen.DashPattern = new float[] { 1f, 2f };
            for (int i = 1; i <= maxCountY; i++)//画y轴刻度
            {
                gph.DrawString((configVO.minY + i * configVO.scaleY).ToString(), new Font("宋体", 11), Brushes.Black, new PointF(left_down.X - 40, left_down.Y - i * spaceY - 6));
                gph.DrawLine(virtualLinePen, left_down.X - 3, left_down.Y - i * spaceY, configVO.grap_width - 40, left_down.Y - i * spaceY);
            }
            StringFormat strF = new StringFormat(StringFormatFlags.DirectionVertical);

            for (int i = 1; i <= maxCountX; i++)//画x轴刻度
            {
                gph.DrawString((i * configVO.scaleX).ToString(), new Font("宋体", 11), Brushes.Black, new PointF(left_down.X + (i) * spaceX - 6, left_down.Y), strF);
                gph.DrawLine(virtualLinePen, left_down.X + (i) * spaceX, left_down.X + 3, left_down.X + (i) * spaceX, configVO.grap_height - 40);
            }


            if (configVO.max_min_flag == true)
            {
                //画当前最大值、最小值   历史最大值最小值
                //gph.DrawLine(Pens.Green, left_down.X, left_down.Y - (curMaxValue - configVO.minY) * stepY, right_down.X, right_down.Y - (curMaxValue - configVO.minY) * stepY);
                //gph.DrawLine(Pens.Green, left_down.X, left_down.Y - (curMinValue - configVO.minY) * stepY, right_down.X, right_down.Y - (curMinValue - configVO.minY) * stepY);
                Font font = new Font("宋体", 10, FontStyle.Bold);
                gph.DrawLine(Pens.Red, left_down.X + curMaxValueIndex * stepX, left_down.Y, left_down.X + curMaxValueIndex * stepX, configVO.grap_height - left_down.Y);
                gph.DrawLine(Pens.Red, left_down.X, left_down.Y - (curMaxValue - configVO.minY) * stepY, right_down.X, right_down.Y - (curMaxValue - configVO.minY) * stepY);
                gph.DrawString("最大值：X:" + curMaxValueIndex.ToString() + "\tY:" + curMaxValue.ToString(), font, Brushes.Black,
                    new PointF(left_down.X + curMaxValueIndex * stepX + 10, right_down.Y - (curMaxValue - configVO.minY) * stepY - 20));
                if (Math.Abs(curMinValue) > 0)
                {
                    gph.DrawLine(Pens.Blue, left_down.X + curMinValueIndex * stepX, left_down.Y, left_down.X + curMinValueIndex * stepX, configVO.grap_height - left_down.Y);
                    gph.DrawLine(Pens.Blue, left_down.X, left_down.Y - (curMinValue - configVO.minY) * stepY, right_down.X, right_down.Y - (curMinValue - configVO.minY) * stepY);
                }

                gph.DrawString("最小值：X:" + curMinValueIndex + "\tY:" + curMinValue.ToString(), font, Brushes.Black,
                    new PointF(left_down.X + curMinValueIndex * stepX + 10, right_down.Y - (curMinValue - configVO.minY) * stepY + 10));
            }

            PointF show_color = new PointF(0,10);//左上
            for (int j = 0; j < configVO.channelNum; j++)
            {
                if (!configVO.channel1 && j == 0) continue;
                if (!configVO.channel2 && j == 1) continue;
                if (!configVO.channel3 && j == 2) continue;
                if (!configVO.channel4 && j == 3) continue;
                //show_color.X = show_color.X + (j * 100);
                show_color.X =  (j * 100)+10;
                gph.DrawString("通道" + (j + 1).ToString() + ":", new Font("宋体", 10), Brushes.Black,show_color);
                gph.DrawLine(new Pen(dataColor[j], 3f), show_color.X + 44, show_color.Y + 7, show_color.X + 80, show_color.Y + 7);
                for (int i = 0; i < dataArrayIndex; i++)
                {

                    //画点
                    //PointF cur = new PointF(left_down.X + i * spaceX - 1.5f, left_down.Y - (dataArray[j,(i - 1)] - configVO.minY) * step - 1.5f);//当前值所处坐标
                    PointF cur = new PointF(left_down.X + (i) * stepX, left_down.Y - (dataArray[j, i] - configVO.minY) * stepY);//当前值所处坐标
                    //gph.DrawEllipse(new Pen(dataColor[j], 3), cur.X, cur.Y, 1, 1);
                   // gph.FillEllipse(new SolidBrush(Color.Black), cur.X, cur.Y, 3, 3);
                    //画数值
                    //gph.DrawString(d[i - 1].ToString(), new Font("宋体", 11), Brushes.Black, new PointF(left_down.X + i * 30, left_down.Y - d[i - 1] * 3));
                    //画折线
                    if (i > 0)
                    {
                        PointF last = new PointF(left_down.X + (i - 1) * stepX, left_down.Y - (dataArray[j, (i - 1)] - configVO.minY) * stepY);//当前值所处坐标
                        gph.DrawLine(new Pen(dataColor[j], 3f), last.X, last.Y, cur.X, cur.Y);
                    }
                }
            }
            //保存输出图片
            //bmap.Save(Response.OutputSt(e-1) ImageFormat.Gif);
            return bmap;

        }
    }
}
