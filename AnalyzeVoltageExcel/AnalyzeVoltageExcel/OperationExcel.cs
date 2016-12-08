using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel; 
using System.IO;
using System.Reflection;

namespace AnalyzeVoltageExcel
{
    public class OperationExcel
    {
        String filePatch = ""; 
        _Worksheet sheet;
        Workbooks workBooks;
        Workbook workBook;
        public void openExcel(OperationParaVO opVO)
        {
            filePatch = opVO.filePath;
            workBooks = new Application().Workbooks;
            workBook = workBooks.Add(filePatch); 
            sheet = workBook.Sheets.get_Item(opVO.sheetIndex);  
        }

        public void analyzeExcel(OperationParaVO opVO)
        {
            int beginLine, endLine, groupLine;
            opVO.maxLine = sheet.UsedRange.CurrentRegion.Rows.Count - 1;
             
            beginLine = ((int)(opVO.maxLine * opVO.topline / 100)) +2;
            endLine = ((int)(opVO.maxLine - opVO.maxLine * opVO.topline / 100)) + 2;
            groupLine = (endLine - beginLine) / opVO.avgline;
            if (((endLine - beginLine) % opVO.avgline != 0))
                groupLine += 1;

            int sumVal = 0,j=0;
            String tmpCell;
            for (int i = beginLine; i < endLine; i++) {  
                tmpCell = ((Range)sheet.Cells[i, opVO.selectColumn]).Value;
                tmpCell = tmpCell.Substring(0,tmpCell.Length-2);
                sumVal += (int.Parse(tmpCell));
                j++;
                if (j % groupLine == 0 && j != 0)
                {
                    opVO.resList.Add(sumVal / groupLine);
                    sumVal = j = 0; 
                }
            }
            if(j % groupLine != 0 && j != 0)
                opVO.resList.Add(sumVal / j);
        }

        public void buildFile(OperationParaVO opVO)
        {
            sheet.Copy(Missing.Value, workBook.Sheets[workBook.Sheets.Count]);
            sheet.Name = "解析结果";
            sheet.Columns.Clear();
            sheet.Rows.Clear(); 
             
            for (int i = 0; i < opVO.resList.Count; i++)
            {
                sheet.Cells[i+1, 1] = opVO.resList[i]; 
            }




            
            workBook.Close();
            workBooks.Close();
            /**/

            /*object missing=System.Reflection.Missing.Value;
            Application app = new Application();
            app.Application.Workbooks.Add(true);
            Workbook book =(Workbook)app.ActiveWorkbook;
            Worksheet sheet = (Worksheet)book.ActiveSheet;
            sheet.Cells[1, 1] = "源数据站点名称记录";
            sheet.Cells[1, 2] = "匹配总数";
            //将DataTable赋值给excel
            
            //保存excel文件
            book.SaveCopyAs("D:\\source.xls");
            //关闭文件
            book.Close(false, missing, missing);
            //退出excel
            app.Quit();*/
        }
         
        //public EmployeeInfo FetchExcel()
        //{
        //    int i = shs.Count;
        //    _Worksheet _wsh = (_Worksheet)shs.get_Item(i);
        //    if (((Range)_wsh.Cells[lineNum, 1]).Value == null)
        //        return null;
        //    EmployeeInfo eiVO = new EmployeeInfo();
        //    eiVO.kqhm = ((Range)_wsh.Cells[lineNum, 1]).Value.ToString();
        //    eiVO.zdybh = ((Range)_wsh.Cells[lineNum, 2]).Value.ToString();
        //    eiVO.name = ((Range)_wsh.Cells[lineNum, 3]).Value.ToString();
        //    eiVO.cqtime = ((Range)_wsh.Cells[lineNum, 4]).Value.ToString();
        //    eiVO.cqDateTime = DateTime.Parse(eiVO.cqtime);
        //    eiVO.cqstate = ((Range)_wsh.Cells[lineNum, 5]).Value.ToString();
        //    eiVO.gzstate = ((Range)_wsh.Cells[lineNum, 6]).Value.ToString();
        //    eiVO.ycqk = ((Range)_wsh.Cells[lineNum, 7]).Value.ToString();
        //    eiVO.dayOfMonth = eiVO.cqDateTime.Day;
        //    lineNum++;
        //    return eiVO;

        //}



        //public String ReadEmployeeInfo()//ReadEmployeeInfo
        //{
        //    while (true)
        //    {
        //        EmployeeInfo eiVO = FetchExcel();
        //        if (eiVO == null)
        //            break;
        //        String name = eiVO.name;
        //        String cqtime = eiVO.cqtime;
        //        DateTime curDateTime = DateTime.Parse(cqtime);

        //        //加班跨天情况判断
        //        if(curDateTime.Hour < 6){
        //            int dataIndex = FindEmployee(name, curDateTime.AddDays(-1).Day);
        //            if(dataIndex == -1){
        //                continue;
        //            }else{
        //                data[dataIndex].setTime(curDateTime);
        //                continue;
        //            }
        //        }
        //        //首先判断此人是否已经读取
        //        int eiIndex = FindEmployee(eiVO.name, curDateTime.Day);
        //        if (eiIndex == -1)
        //        {
        //            data.Add(eiVO);
        //            continue;
        //        }
        //        else {
        //            data[eiIndex].setTime(curDateTime);
        //        }
        //    }
        //    String res = "姓名：\t上班时间：\t\t 下班时间：\t\t工作时间：\r\n";
        //    foreach (EmployeeInfo curEI in data)
        //    {
        //        String down = "";
        //        if (curEI.xbDateTime.Year == 1)
        //            down = "无记录";
        //        else
        //            down = curEI.xbDateTime.ToString();
        //        res += (curEI.name + "\t" + curEI.cqDateTime.ToString() + "\t" + down + "\t" + curEI.workMinutes + "\r\n");
        //    }
        //    return res;
        //}

        //public String analysisWork()
        //{
        //    String res = "姓名：\t工作时间：\t出勤天数：\r\n";
        //    /*foreach (EmployeeInfo curEI in data)
        //    {
        //        res += (curEI.name + "\t" + curEI.cqDateTime.ToString() + "\t" + curEI.xbDateTime.ToString() +"\t"+curEI.workMinutes + "\r\n");
        //    }
        //    return res;*/
        //    EmployeeCount ec = new EmployeeCount();
        //    foreach (EmployeeInfo curEI in data) {
        //        if (ec.name!=null && ec.name != curEI.name)
        //        {
        //            ECs.Add(ec);
        //            res += ec.name + "\t" + ec.sumAll+ "\t\t"+ ec.days + "\r\n";
        //            ec = new EmployeeCount();
        //        }
        //        ec.name = curEI.name;
        //        ec.sumAll += curEI.workMinutes;
        //        ec.days++;
        //    }
        //    ECs.Add(ec);
        //    res += ec.name + "\t" + ec.sumAll + "\t\t" + ec.days + "\r\n";
        //    return res;
        //}



        /*
         public String analysisWork(){
             int lineNum = 2;
            
             EmployeeInfo eiVO;
             Sheets shs = _wbk.Sheets;
             int i = shs.Count;
             _Worksheet _wsh = (_Worksheet)shs.get_Item(i);
             while (true) {
                 if (((Range)_wsh.Cells[lineNum, 1]).Value == null)
                     break;
                 eiVO = new EmployeeInfo();
                 eiVO.kqhm    = ((Range)_wsh.Cells[lineNum, 1]).Value.ToString();
                 eiVO.zdybh   = ((Range)_wsh.Cells[lineNum, 2]).Value.ToString();
                 eiVO.name    = ((Range)_wsh.Cells[lineNum, 3]).Value.ToString();
                 eiVO.cqtime  = ((Range)_wsh.Cells[lineNum, 4]).Value.ToString();
                 eiVO.cqDateTime = DateTime.Parse(eiVO.cqtime);
                 eiVO.cqstate = ((Range)_wsh.Cells[lineNum, 5]).Value.ToString();
                 eiVO.gzstate = ((Range)_wsh.Cells[lineNum, 6]).Value.ToString();
                 eiVO.ycqk    = ((Range)_wsh.Cells[lineNum, 7]).Value.ToString();
                 data.Add(eiVO);
                 lineNum++;
                 if (maxDateTime.Year == 1)
                     maxDateTime = eiVO.cqDateTime;
                 else
                     maxDateTime = eiVO.cqDateTime > maxDateTime?eiVO.cqDateTime:maxDateTime;
                 if (minDateTime.Year == 1)
                     minDateTime = eiVO.cqDateTime;
                 else
                     minDateTime = eiVO.cqDateTime < minDateTime ? eiVO.cqDateTime : minDateTime;
             }
             //return minDateTime.ToString() + "-----" + maxDateTime.ToString();

             String curName = "";
             String res = "姓名\tDAY1\tDAY2\tDAY3\tDAY4\tDAY5\tDAY6\tDAY7\t总计\r\n";
             EmployeeCount EC = new EmployeeCount();
             List<EmployeeCount> ECs = new List<EmployeeCount>();
             foreach(EmployeeInfo curEI in data){
                 if ((!curName.Equals("")) && curName != curEI.name)
                 {
                     EC.sumH = EC.day1 + EC.day2 + EC.day3 + EC.day4 + EC.day5 + EC.day6 + EC.day7;
                     ECs.Add(EC);
                     res += EC.name + "\t"
                         + EC.day1 + "\t"
                         + EC.day2 + "\t"
                         + EC.day3 + "\t"
                         + EC.day4 + "\t"
                         + EC.day5 + "\t"
                         + EC.day6 + "\t"
                         + EC.day7 + "\t" 
                         + EC.sumH + "\r\n";
                     EC = new EmployeeCount();
                 }
                 curName = EC.name = curEI.name;
                 int dayOffset = curEI.cqDateTime.Day - minDateTime.Day;
                 if (dayOffset == 0 || (dayOffset == 1 && curEI.cqDateTime.Hour < 6))
                 {
                     if (EC.day1_U.Year == 1)
                     {
                         EC.day1_U = curEI.cqDateTime;
                     }
                     else
                     {
                         EC.day1_D = curEI.cqDateTime;
                         EC.day1 = (EC.day1_D - EC.day1_U).Days*1440 + (EC.day1_D - EC.day1_U).Hours * 60 + (EC.day1_D - EC.day1_U).Minutes;
                     }
                 }
                 else if (dayOffset == 1 || (dayOffset == 2 && curEI.cqDateTime.Hour < 6))
                 {
                     if (EC.day2_U.Year == 1)
                     {
                         EC.day2_U = curEI.cqDateTime;
                     }
                     else
                     {
                         EC.day2_D = curEI.cqDateTime;
                         EC.day2 = (EC.day2_D - EC.day2_U).Days * 1440 + (EC.day2_D - EC.day2_U).Hours * 60 + (EC.day2_D - EC.day2_U).Minutes;
                     }
                 }
                 else if (dayOffset == 2 || (dayOffset == 3 && curEI.cqDateTime.Hour < 6))
                 {
                     if (EC.day3_U.Year == 1)
                     {
                         EC.day3_U = curEI.cqDateTime;
                     }
                     else
                     {
                         EC.day3_D = curEI.cqDateTime;
                         EC.day3 = (EC.day3_D - EC.day3_U).Days * 1440 + (EC.day3_D - EC.day3_U).Hours * 60 + (EC.day3_D - EC.day3_U).Minutes;
                     }
                 }
                 else if (dayOffset == 3 || (dayOffset == 4 && curEI.cqDateTime.Hour < 6))
                 {
                     if (EC.day4_U.Year == 1)
                     {
                         EC.day4_U = curEI.cqDateTime;
                     }
                     else
                     {
                         EC.day4_D = curEI.cqDateTime;
                         EC.day4 = (EC.day4_D - EC.day4_U).Days * 1440 + (EC.day4_D - EC.day4_U).Hours * 60 + (EC.day4_D - EC.day4_U).Minutes;
                     }
                 }
                 else if (dayOffset == 4 || (dayOffset == 5 && curEI.cqDateTime.Hour < 6))
                 {
                     if (EC.day5_U.Year == 1)
                     {
                         EC.day5_U = curEI.cqDateTime;
                     }
                     else
                     {
                         EC.day5_D = curEI.cqDateTime;
                         EC.day5 = (EC.day5_D - EC.day5_U).Days * 1440 + (EC.day5_D - EC.day5_U).Hours * 60 + (EC.day5_D - EC.day5_U).Minutes;
                     }
                 }
                 else if (dayOffset == 5 || (dayOffset == 6 && curEI.cqDateTime.Hour < 6))
                 {
                     if (EC.day6_U.Year == 1)
                     {
                         EC.day6_U = curEI.cqDateTime;
                     }
                     else
                     {
                         EC.day6_D = curEI.cqDateTime;
                         EC.day6 = (EC.day6_D - EC.day6_U).Days * 1440 + (EC.day6_D - EC.day6_U).Hours * 60 + (EC.day6_D - EC.day6_U).Minutes;
                     }
                 }
                 else if (dayOffset == 6 || (dayOffset == 7 && curEI.cqDateTime.Hour < 6))
                 {
                     if (EC.day7_U.Year == 1)
                     {
                         EC.day7_U = curEI.cqDateTime;
                     }
                     else
                     {
                         EC.day7_D = curEI.cqDateTime;
                         EC.day7 = (EC.day7_D - EC.day7_U).Days * 1440 + (EC.day7_D - EC.day7_U).Hours * 60 + (EC.day7_D - EC.day7_U).Minutes;
                     }
                 }

             }






             return res;
             //return ((Range)_wsh.Cells[1, 1]).Value.ToString();
         }*/



    }
}
