using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LineChar
{
    public class ConfigVO
    {
        public int grap_width;//画布宽
        public int grap_height;//画布高
        public int minY;//Y轴最小值
        public float scaleX;//X轴刻度
        public float scaleY;//Y轴刻度
        public int channelNum;//通道数
        public int datasize;//图点缓冲区大小
        public bool max_min_flag;//是否打开最大最小值统计
    }
}
