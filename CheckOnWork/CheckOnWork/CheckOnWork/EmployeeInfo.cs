using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CheckOnWork
{
    class EmployeeInfo
    {
        public String kqhm = "";
        public String zdybh = "";
        public String name = "";
        public String cqtime = "";
        public int dayOfMonth;
        public DateTime cqDateTime;
        public DateTime xbDateTime;
        public String cqstate = "";
        public String gzstate = "";
        public String ycqk = "";
        public int workMinutes;

        public void setTime(DateTime curDateTime)
        {
            if (curDateTime.Hour < 6)
            {//如果小于7点则直接放入下班时间
                xbDateTime = curDateTime;
            }

            if (cqDateTime.Year == 1)
            {
                cqDateTime = curDateTime;
            }
            else
            {
                if (curDateTime < cqDateTime)
                    cqDateTime = curDateTime;
                if (curDateTime > xbDateTime)
                    xbDateTime = curDateTime;
            }

            if(cqDateTime.Year != 1 && xbDateTime.Year!=1){
                TimeSpan offset = (TimeSpan)(xbDateTime - cqDateTime);
                workMinutes = offset.Days * 24 * 60 + offset.Hours * 60 + offset.Minutes;
                Debug.WriteLine(workMinutes + " " + offset.Days + "" + offset.Hours);
            }
        }
    }

    /*class EmployeeCount {
        public String name;
        public float day1;
        public DateTime day1_U, day1_D;
        public float day2;
        public DateTime day2_U, day2_D;
        public float day3;
        public DateTime day3_U, day3_D;
        public float day4;
        public DateTime day4_U, day4_D;
        public float day5;
        public DateTime day5_U, day5_D;
        public float day6;
        public DateTime day6_U, day6_D;
        public float day7;
        public DateTime day7_U, day7_D;
        public float sumH;
        
    }*/
    class EmployeeCount {
     public String name;
     public float sumAll;
     public int days;
        
 }
}
