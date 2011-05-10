using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using CookImplement.Dialog;

namespace CookImplement
{
    public class Rank
    {
        public String rName;
        public Char rNumber;
        public Int32 rIndex;
        public Image rImage;

        public enum rNames
        { 혼합 = 0, 굽기 = 1, 삶기 = 2, 반죽 = 3, 끓이기 = 4, 면 = 5, 튀기기 = 6, 볶기 = 7, 파스타 = 8, 잼 = 9, 파이 = 10 };

        public Rank(Char RankNumber)
        {
            
            this.rNumber = RankNumber;
        }
        public Rank(int RankNumber)
        {

        }

        public static char checkRank(int i)
        {
            switch (i)
            {
                case 0: return 'P';
                case 1: return 'F';
                case 2: return 'E';
                case 3: return 'D';
                case 4: return 'C';
                case 5: return 'B';
                case 6: return 'A';
                case 7: return '9';
                case 8: return '8';
                case 9: return '7';
                case 10: return '6';
                default: return '\0';
            }
        }
        public static int checkRank(char c)
        {
            switch (c)
            {
                case 'P': return 0;
                case 'F': return 1;
                case 'E': return 2;
                case 'D': return 3;
                case 'C': return 4;
                case 'B': return 5;
                case 'A': return 6;
                case '9': return 7;
                case '8': return 8;
                case '7': return 9;
                case '6': return 10;
                default: return -1;
            }
        }
    }
    public class Cook
    {
        public enum RankName 
        { 혼합 = 0, 굽기 = 1, 삶기 = 2, 반죽 = 3, 끓이기 = 4, 면 = 5, 튀기기 = 6, 볶기 = 7, 파스타 = 8, 잼 = 9, 파이 = 10 };
        private int cNumber;
        private String cName;
        private String cRank;
        private int[] cEffect;
        private String cSpecialEffect = "";
        private String[] cStuff;
        private int[] cStuffRatio;

        public Cook(string data)
        {
            RefreshData(data);
        }
        public Cook()
        {
        }

        public void RefreshData(string s)
        {
            short foodindex = 0, i = 0, j = 0;
            string[] datas = s.Split(';');
            string[] buffer;
            cEffect = new int[11];
            int[] intbuffer = new int[3];
            try
            {
                for (j = 0; j < cEffect.Length; j++) cEffect[j] = 0;
                while (i < 5)
                {
                    switch (i)
                    {
                        case 0:
                            cNumber = foodindex;
                            cName = datas[i];
                            break;
                        case 1:
                            cRank = datas[i];
                            break;
                        case 2:
                            cStuff = datas[i].Split(',');
                            break;
                        case 3:     // 091120 Fix : Data File 구조 변경
                            buffer = datas[i].Split(',');
                            for (j = 0; j < buffer.Length; j++)
                            {
                                string[] parsebuffer = buffer[j].Split(':');
                                int parsemode = int.Parse(parsebuffer[0]);
                                if (parsemode == 0)
                                {
                                    if (parsebuffer[1].Equals("1"))
                                    {
                                        cSpecialEffect += "요리재료";
                                        cEffect[parsemode] += 1;
                                    }
                                    else
                                    {
                                        cSpecialEffect += parsebuffer[1];
                                        if (!parsebuffer[1].Contains("없음")) cEffect[parsemode] += 2;
                                        else cEffect[parsemode] = -1;
                                    }
                                }
                                else if (parsemode == 10)
                                {
                                    cEffect[parsemode] = int.Parse(parsebuffer[1]);
                                    cSpecialEffect += "다이어트 효과";
                                }
                                else
                                {
                                    cEffect[parsemode] = int.Parse(parsebuffer[1]);
                                }
                            }
                            break;
                        case 4:
                            buffer = datas[i].Split(',');
                            j = 0;
                            while (j < 3)
                            {
                                intbuffer[j] = int.Parse(buffer[j]);
                                j++;
                            }
                            cStuffRatio = intbuffer;
                            break;
                    }
                    i++;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                
                ErrorDialog error = new ErrorDialog(s, e.StackTrace); error.ShowDialog();
            }
        }
        public static char checkRank(int i)
        {
            switch (i)
            {
                case 0: return 'P';
                case 1: return 'F';
                case 2: return 'E';
                case 3: return 'D';
                case 4: return 'C';
                case 5: return 'B';
                case 6: return 'A';
                case 7: return '9';
                case 8: return '8';
                case 9: return '7';
                case 10: return '6';
                default: return '\0';
            }
        }
        public static int checkRank(char c)
        {
            switch (c)
            {
                case 'P': return 0;
                case 'F': return 1;
                case 'E': return 2;
                case 'D': return 3;
                case 'C': return 4;
                case 'B': return 5;
                case 'A': return 6;
                case '9': return 7;
                case '8': return 8;
                case '7': return 9;
                case '6': return 10;
                default: return -999;
            }
        }

        public char Rank
        {
            get
            {
                char[] buffer = cRank.ToCharArray();
                return buffer[0];
            }
        }
        public String Name
        {
            get { return cName; }
        }
        public String[] Stuff
        {
            get { return cStuff; }
        }
        public String SEffect
        {
            get { return cSpecialEffect; }
        }

        public int getNumber()
        {
            return cNumber;
        }
        public char getRank()
        {
            char[] buffer = cRank.ToCharArray();
            return buffer[0];
        }
        public String getRankName()
        {
            return "";
        }
        public String getName()
        {
            return cName;
        }
        public String[] getStuff()
        {
            return cStuff;
        }
        public String getStuffForString()
        {
            String buffer = "";
            for (int i = 0; i < cStuff.Length; i++)
            {
                buffer += cStuff[i];
                if (i == 0) buffer += " / ";
                else if (i == 1 && cStuff.Length == 3) buffer += " / ";
            }
            return buffer;
        }
        public int[] getEffectArray()
        {
            return cEffect;
        }
        public String getEffectToInt()
        {
            StringBuilder buffer = new StringBuilder();
            for (int i = 0; i < cEffect.Length; i++)
            {
                if (cEffect[i] != 0)
                {
                    if (i == 0)
                    {
                        if (cEffect[i] == 1) buffer.Append(i + ":요리 재료");
                        else if (cEffect[i] == 2) buffer.Append(i + ":" + cSpecialEffect);
                        else if (cEffect[i] == -1) buffer.Append(i + ":효과 없음");
                    }
                    else if (i == 10) buffer.Append(i + ":7");
                    else
                    {
                        buffer.Append(i + ":");
                        if (cEffect[i] > 0) buffer.Append("+" + cEffect[i]);
                        else buffer.Append(cEffect[i]);
                    }
                    buffer.Append(',');
                }
            }
            buffer.Remove(buffer.Length - 1, 1);
            buffer.Append(';');
            return buffer.ToString();
        }
        public String getEffect()
        {
            StringBuilder buffer = new StringBuilder();
            for (int i = 0; i < cEffect.Length; i++)
            {
                if (cEffect[i] != 0)
                {
                    if (!(buffer.Length == 0)) buffer.Append(", ");
                    switch (i)
                    {
                        case 0:
                            if (cEffect[i] == 1)
                            {
                                buffer.Append("요리재료로 쓰임");
                                break;
                            }
                            else
                            {
                                buffer.Append(cSpecialEffect);
                                break;
                            }
                        case 1:
                            buffer.Append("체력 ");
                            break;
                        case 2:
                            buffer.Append("지력 ");
                            break;
                        case 3:
                            buffer.Append("솜씨 ");
                            break;
                        case 4:
                            buffer.Append("의지 ");
                            break;
                        case 5:
                            buffer.Append("행운 ");
                            break;
                        case 6:
                            buffer.Append("최대생명력 ");
                            break;
                        case 7:
                            buffer.Append("최대마나 ");
                            break;
                        case 8:
                            buffer.Append("최대스태미너 ");
                            break;
                        case 9:
                            buffer.Append("스태미너 회복도 ");
                            break;
                        case 10:
                            if (cEffect[i] > 0)
                                buffer.Append("다이어트 효과");
                            break;
                    }
                    if (i != 0 && i != 10)
                    {
                        if (cEffect[i] > 0)
                            buffer.Append("+" + cEffect[i]);
                        else
                            buffer.Append(cEffect[i]);
                    }
                }
            }
            /*              if (cSpecialEffect != "")
                            {
                                if (buffer != null) buffer.Append(", " + cSpecialEffect);
                                else buffer.Append(cSpecialEffect);
                            } */
            return buffer.ToString();
        }
        public int[] getRatio()
        {
            return cStuffRatio;
        }

        override public String ToString()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(cName + ";");
            buffer.Append(cRank + ";");
            for (int i = 0; i < cStuff.Length; i++)
            {
                buffer.Append(cStuff[i]);
                if (i == 0) buffer.Append(',');
                else if (i == 1 && cStuff.Length == 3) buffer.Append(',');
            }
            buffer.Append(';');
            buffer.Append(this.getEffectToInt());
            for (int i = 0; i < cStuffRatio.Length; i++)
            {
                buffer.Append(cStuffRatio[i]);
                if (i != cStuffRatio.Length - 1) buffer.Append(',');
            }
            return buffer.ToString();
        }
        override public bool Equals(Object o)
        {
            Cook buffer = (Cook)o;
            if (this.cName.Equals(buffer.getName()))
                return true;
            else
                return false;
        }
		override public int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
    public class CookData
    {
        private Cook SelectedCook; private int CookIndex;
        private Stuff[] SelectedStuffs = new Stuff[3];
        private List<Cook> CookList;
        private List<Stuff> StuffList;
        private String ChkKeyword;

        public Cook cook
        {
            get { return SelectedCook; }
        }
        public int cIndex
        {
            get { return CookIndex; }
        }
        public Stuff[] stuffs
        {
            get { return SelectedStuffs; }
        }
        public List<Cook> cList
        {
            get { return CookList; }
        }
        public List<Stuff> sList
        {
            get { return StuffList; }
        }

        public CookData(List<Cook> CList, List<Stuff> SList, int index)
        {
            this.CookList = CList;
            this.StuffList = SList;
            this.CookIndex = index;
            this.SelectedCook = this.CookList[this.CookIndex];
            CheckStuff();
        }
        public CookData(List<Cook> CList, List<Stuff> SList, String name)
        {
            this.CookList = CList;
            this.StuffList = SList;
            this.ChkKeyword = name;
            this.CookIndex = this.CookList.FindIndex(FindCookName);
            this.SelectedCook = this.CookList[CookIndex];
            CheckStuff();
        }
        public CookData(List<Cook> CList, List<Stuff> SList, Cook cook)
        {
            this.CookList = CList;
            this.StuffList = SList;
            this.ChkKeyword = cook.getName();
            this.CookIndex = this.CookList.FindIndex(FindCookName);
            this.SelectedCook = this.CookList[CookIndex];
            CheckStuff();
        }

        private void CheckStuff()
        {
            String[] buffer = SelectedCook.getStuff();
            for (int i = 0; i < buffer.Length; i++)
            {
                this.ChkKeyword = buffer[i];
                SelectedStuffs[i] = StuffList.Find(FindStuffName);
            }
        }
        private bool FindCookName(Cook o)
        {
            if (o.getName() == ChkKeyword) return true;
            else return false;
        }
        private bool FindStuffName(Stuff o)
        {
            if (o.sName == ChkKeyword) return true;
            else return false;
        }
    }
    public class Stuff
    {
        public enum SellerName
        {
            케이틴 = 0, 글리니스 = 1, 세나 = 2, 글루아스 = 3, 리리스 = 4,
            에피 = 5, 제니퍼 = 6, 루와이 = 7, 피에릭 = 8, 글라니테스 = 9,
            고든 = 10, 프레이저 = 11, 반스트 = 12, 채집 = 13, 요리 = 14, 
            루카스 = 15, 카독 = 16
        };
        private String Name;
        private int Price;
        private List<SellerName> SellingPoint = new List<SellerName>();

        public String sName
        {
            get { return this.Name; }
        }
        public int sPrice
        {
            get { return this.Price; }
        }
        public List<SellerName> sSellingPoint
        {
            get { return this.SellingPoint; }
        }

        /// <summary>
        /// Make object through 'Console Input'
        /// Only for using testments.
        /// </summary>
        public Stuff()
        {
            try
            {
                Console.Write("재료 이름을 입력하세요.\n# ");
                this.Name = Console.ReadLine();
                Console.Write("재료 가격을 입력하세요.\n# ");
                this.Price = Int32.Parse(Console.ReadLine());
                Console.Write("재료 입수 방법을 입력하세요.\n ");
                Console.Write("케이틴 = 0, 글리니스 = 1, 세나 = 2, 글루아스 = 3, 리리스 = 4, \n에피 = 5, 제니퍼 = 6, 루와이 = 7, 피에릭 = 8, 글라니테스 = 9, \n반스트 = 10, 고든 = 11, 프레이저 = 12, 채집 = 13, 요리 = 14 \n#");
                int i = -1;
                i = Int32.Parse(Console.ReadLine());
                while (i >= 0 && i <= 15)
                {
                    SellingPoint.Add((SellerName)Enum.Parse(typeof(SellerName), i.ToString()));
                    Console.Write("# ");
                    i = Int32.Parse(Console.ReadLine());
                }
                Console.WriteLine(this.ToString());
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("입력한 데이터의 형식이 잘못 되었습니다.");
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("계속 진행하려면 아무 키나 누르세요.");
            }
        }
        /// <summary>
        /// Make object through 'Passed Data'
        /// </summary>
        /// <param name="data">Serialized Data</param>
        public Stuff(String data)
        {
            try
            {
                String[] StuffDatas = data.Split('/');
                if (StuffDatas.Length == 3)
                {
                    String[] SellerDatas = StuffDatas[2].Split(',');
                    this.Name = StuffDatas[0];
                    this.Price = Int32.Parse(StuffDatas[1]);
                    for (int i = 0; i < SellerDatas.Length; i++) SellingPoint.Add((SellerName)Enum.Parse(typeof(SellerName), SellerDatas[i]));
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("데이터 파일의 형식이 잘못 되었습니다.");
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("계속 진행하려면 아무 키나 누르세요.");
            }
        }
        public override String ToString()
        {
            int i = 0; String sellers = "";
            while (i < SellingPoint.Count)
            {
                sellers += SellingPoint[i++].ToString();
                if (i != SellingPoint.Count) sellers += ',';
            }
            return Name + "/" + Price + "/" + sellers;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}