using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyzeVoltageExcel
{
    public class OperationParaVO
    {
        public OperationParaVO() {
            resList = new List<int>();
        }
        public float topline;
        public float endline;
        public int avgline;
        public int maxLine;
        public int selectColumn;

        public String filePath;
        public int sheetIndex;

        public List<int> resList;
        
    }
}
