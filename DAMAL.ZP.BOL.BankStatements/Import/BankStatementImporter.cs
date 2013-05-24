using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using DAMAL.ZP.BOL.BankStatements.Import.BankSpodzielczy;
using DAMAL.ZP.BOL.BankStatements.Import.BPH;
using DAMAL.ZP.BOL.BankStatements.Import.Bre;
using DAMAL.ZP.BOL.BankStatements.Import.Citi;
using DAMAL.ZP.BOL.BankStatements.Import.CitiDirect;
using DAMAL.ZP.BOL.BankStatements.Import.CitiDirect_Swift;
using DAMAL.ZP.BOL.BankStatements.Import.DeutscheBankMT940;
using DAMAL.ZP.BOL.BankStatements.Import.DeutscheBankXML;
using DAMAL.ZP.BOL.BankStatements.Import.FortisMultiCash;
using DAMAL.ZP.BOL.BankStatements.Import.PKOXML;
using DAMAL.ZP.BOL.BankStatements.Import.ING;
using DAMAL.ZP.BOL.BankStatements.Import.Liniowy;
using DAMAL.ZP.BOL.BankStatements.Import.MultiCash;
using DAMAL.ZP.BOL.BankStatements.Import.RaiffeisenOnLine;
using DAMAL.ZP.BOL.BankStatements.Import.TransCollect;
using System.Text;
using DAMAL.ZP.BOL.BankStatements.Import.TransCollect2;
using DAMAL.ZP.BOL.BankStatements.Import.TransDebit;
using DAMAL.ZP.BOL.BankStatements.Import.Societe;
using DAMAL.ZP.BOL.BankStatements.Import.ParibasMT940;
using FKIntegration.Documents;

namespace DAMAL.ZP.BOL.BankStatements.Import
{
    public class EncodingFileType
    {
        private static readonly IEncodingFileTypeData m_data;
        private string m_EncodingName;
        private int m_Id;

        static EncodingFileType()
        {
            //m_data = DataAccessFactory.Get<IEncodingFileTypeData>();
        }

        public EncodingFileType(int id, string name)
        {
            m_Id = id;
            m_EncodingName = name;
        }

        public int Id
        {
            get { return m_Id; }
            set
            {
                if (m_Id != value)
                {
                    m_Id = value;
                }
            }
        }

        public string EncodingName
        {
            get { return m_EncodingName; }
            set
            {
                if (m_EncodingName != value)
                {
                    m_EncodingName = value;
                }
            }
        }

        public Encoding GetEncoding()
        {
            return Encoding.GetEncoding(EncodingName);
        }

        public List<EncodingFileType> Get()
        {
            return m_data.Get();
        }
    }

    public interface IDataAccess
    { }

    public interface IEncodingFileTypeData : IDataAccess
    {

        List<EncodingFileType> Get();
    }

    public class EncodingFileTypeData : IEncodingFileTypeData
    {

        #region IEncodingFileTypeData Members

        public List<EncodingFileType> Get()
        {
            List<EncodingFileType> encodingList = new List<EncodingFileType>();
            encodingList.Add(new EncodingFileType(1, "windows-1250"));
            encodingList.Add(new EncodingFileType(2, "CP850"));
            encodingList.Add(new EncodingFileType(3, "CP852"));
            encodingList.Add(new EncodingFileType(4, "Latin-2"));
            return encodingList;
        }

        #endregion
    }

    public class BankStatementImporter
    {
        private delegate bool Condition(string line);

        public static List<BankStatement> ImportElixMasFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            List<BankStatement> bsList = new List<BankStatement>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName.Dirs, encodingFileType.GetEncoding()))
                {
                    BankStatement bs = new BankStatement("BS");
                    bs.FileName = fileName.GetFileName();
                    bs.Content = "Import z systemu ElixMas";                    
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = DateTimeHelper.ReplaceInStr(line, ',', ' ').Replace(@"""", "");
                        string[] paramArr = line.Split(",".ToCharArray());

                        if (paramArr[0] == "111")
                        {
                            
                            BankStatementPosition bsPosition = new BankStatementPosition();
                            bs.DocumentDate = DateTimeHelper.ToDateTimeLong(paramArr[1]);
                            bsPosition.SideWN.Value = Convert.ToDecimal(paramArr[2]) / 100;
                            bsPosition.SideMA.Value = Convert.ToDecimal(paramArr[2]) / 100;
                            bsPosition.DebitCredit = new DebitCredit("C");
                            bsPosition.Descr = paramArr[11];
                            bsPosition.Customer.BankAccount = paramArr[6];
                            bsPosition.Customer.GetFromString(paramArr[7]);
                            bs.Positions.Add(bsPosition);
                        }
                    }
                    if (bs.Positions.Count > 0)
                        bsList.Add(bs);
                }
            }
            catch (Exception e)
            {
                //fName.HasError(e);
                //throw new Exception("B³ąd czytania pliku: " + fName, e);
            }
            return bsList;
        }

        public static List<BankStatement> ImportRaiffeisenOnLineFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<RaiffeisenMT940BankStatement>(fileName, encodingFileType, delegate(string line)
                {
                    return line.Length > 4 && line.Substring(0, 4) == ":20:";
                });
        }

        public static List<BankStatement> ImportMultiCashFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<MultiCashMT940BankStatement>(fileName, encodingFileType, delegate(string line)
                {
                    return line.Length > 4 && line.Substring(0, 4) == ":20:" && line != ":20:STARTDISP";
                });
        }

        public static List<BankStatement> ImportLiniowyFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<LiniowyBankStatement>(fileName, encodingFileType, ParseLiniowyBankStatements);
        }

        private static List<LiniowyBankStatement> ParseLiniowyBankStatements(ImportFile fileName, EncodingFileType encodingFileType)
        {
            List<LiniowyBankStatement> bsList = new List<LiniowyBankStatement>();
            // niestandardowy typkodaowani apliku ISO-8859-2
            using (StreamReader sr = new StreamReader(fileName.Dirs, Encoding.GetEncoding("ISO-8859-2")))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    LiniowyBankStatement bs = new LiniowyBankStatement();
                    bs.SetImportFileName(fileName);
                    if (bs.ReadStream(sr, ref line))
                        bsList.Add(bs);
                }
            }


            return bsList;
        }
        //TransCollect
        public static List<BankStatement> ImportTransCollectFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<TransCollectBankStatement>(fileName, encodingFileType, ParseTransCollectBankStatements);
        }
        //TransCollect2
        public static List<BankStatement> ImportTransCollect2Files(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<TransCollect2BankStatement>(fileName, encodingFileType, ParseTransCollect2BankStatements);
        }
        //TransDebit
        public static List<BankStatement> ImportTransDebitFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<TransDebitBankStatement>(fileName, encodingFileType, ParseTransDebitBankStatements);
        }

        private static List<TransDebitBankStatement> ParseTransDebitBankStatements(ImportFile fileName, EncodingFileType encodingFileType)
        {
            List<TransDebitBankStatement> bsList = new List<TransDebitBankStatement>();
            // niestandadowy typ kodowania pliku Windows-1250
            using (StreamReader sr = new StreamReader(fileName.Dirs, Encoding.GetEncoding("Windows-1250")))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    TransDebitBankStatement bs = new TransDebitBankStatement();
                    bs.SetImportFileName(fileName);
                    if (bs.ReadStream(sr, ref line))
                        bsList.Add(bs);
                }
            }


            return bsList;
        }

        private static List<TransCollectBankStatement> ParseTransCollectBankStatements(ImportFile fileName, EncodingFileType encodingFileType)
        {
            List<TransCollectBankStatement> bsList = new List<TransCollectBankStatement>();
            // niestandadowy typ kodowania pliku Windows-1250
            using (StreamReader sr = new StreamReader(fileName.Dirs, Encoding.GetEncoding("Windows-1250")))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    TransCollectBankStatement bs = new TransCollectBankStatement();
                    bs.SetImportFileName(fileName);
                    if (bs.ReadStream(sr, ref line))
                        bsList.Add(bs);
                }
            }


            return bsList;
        }
        private static List<TransCollect2BankStatement> ParseTransCollect2BankStatements(ImportFile fileName, EncodingFileType encodingFileType)
        {
            List<TransCollect2BankStatement> bsList = new List<TransCollect2BankStatement>();
            // niestandadowy typ kodowania pliku Windows-1250
            using (StreamReader sr = new StreamReader(fileName.Dirs, Encoding.GetEncoding("Windows-1250")))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    TransCollect2BankStatement bs = new TransCollect2BankStatement();
                    bs.SetImportFileName(fileName);
                    if (bs.ReadStream(sr, ref line))
                        bsList.Add(bs);
                }
            }


            return bsList;
        }

        public static List<BankStatement> ImportINGFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<INGMT940BankStatement>(fileName, encodingFileType, delegate(string line)
                {
                    return line.Length > 4 && line.Substring(0, 4) == ":20:";
                });
        }

        public static List<BankStatement> ImportFortisMultiCashFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<FortisMT940BankStatement>(fileName, encodingFileType, delegate(string line)
                {
                    return line.Length > 4 && line.Substring(0, 4) == ":20:" && line != ":20:STARTDISP";
                });
        }

        public static List<BankStatement> ImportCitiHaysFiles(ImportFile fileName, EncodingFileType enodingFileType)
        {
            return ImportFromFiles<CitiBankStatement>(fileName, enodingFileType, ParseCitiBankStatements);
        }

        public static List<CitiBankStatement> ParseCitiBankStatements(ImportFile fileName, EncodingFileType encodingFileType)
        {
            List<CitiBankStatement> bsList = new List<CitiBankStatement>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName.Dirs, encodingFileType.GetEncoding()))
                {
                    string line = sr.ReadToEnd();
                    CitiBankStatement bs = new CitiBankStatement();
                    bs.SetImportFileName(fileName);
                    if (bs.ReadStream(sr, ref line))
                        bsList.Add(bs);
                }
            }
            catch (Exception e)
            { }
            return bsList;
        }

        public static List<BankStatement> ImportCitiDirectFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<CitiDirectMT940BankStatement>(fileName, encodingFileType, delegate(string line)
                {
                    return line.Length > 4 && line.Substring(0, 4) == ":20:" && line != ":20:STARTDISP";
                });
        }

        public static List<BankStatement> ImportCitiDirectSwiftFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<CitiDirectMT940SwiftBankStatement>(fileName, encodingFileType, delegate(string line)
                {
                    return line.Length > 4 && line.Substring(0, 4) == ":20:" && line != ":20:STARTDISP";
                });
        }
        public static List<BankStatement> ImportBreBankFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<BreBankStatement>(fileName, encodingFileType, delegate(string line)
            {
                //return line.Length > 4 && line.Substring(0, 6) == ":20:ST";
                //DL dla Societe Generale
                return line.Length > 4 && line.Substring(0, 4) == ":20:";
            });
        }

        public static List<BankStatement> ImportSocieteBankFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<SocieteBankStatement>(fileName, encodingFileType, delegate(string line)
            {
                //return line.Length > 4 && line.Substring(0, 6) == ":20:ST";
                //DL dla Societe Generale
                return line.Length > 4 && line.Substring(0, 4) == ":20:";
            });
        }

        public static List<BankStatement> ImportBPHFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<BPHBankStatement>(fileName, encodingFileType, delegate(string line)
            {
                return line.Length > 4 && line.Substring(0, 4) == ":20:";
            });
        }

        public static List<BankStatement> ImportDeutscheMT940Files(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<DeutscheBankMT940BankStatementParser>(fileName, new EncodingFileType(17, "ISO-8859-2"), delegate(string line)
            {
                return line.Length > 4 && line.Substring(0, 4) == ":20:";
            });
        }

        public static List<BankStatement> ImportBSMultiCashFiles(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<BSMT940BankStatement>(fileName, encodingFileType, delegate(string line)
            {
                return line.Length > 4 && line.Substring(0, 4) == ":20:" && line != ":20:STARTDISP";
            });
        }

        private static List<T> ImportFromFilesMT940<T>(ImportFile fileName, EncodingFileType encodingFileType, Condition condition) where T : IBankStatementParser, new()
        {
            List<T> bsList = new List<T>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName.Dirs, encodingFileType.GetEncoding()))
                {
                    string line = sr.ReadLine().Trim();
                    if (line.Length > 2 && line.IndexOf("{1:F01") != -1)
                        line = sr.ReadLine().Trim();
                    while (line != null)
                    { 
                        //if (line.Length > 4 && line.Substring(0, 4) == ":20:" && line != ":20:STARTDISP")
                        if (condition(line))
                        {
                            T bs = new T();
                            bs.SetImportFileName(fileName);
                            if (bs.ReadStream(sr, ref line))
                                bsList.Add(bs);
                        }
                        else
                            line = sr.ReadLine();
                    }
                }
            }
            catch (Exception e)
            { }
            return bsList;
        }

        public static List<BankStatement> ImportDeutscheBankXML(ImportFile fileName, EncodingFileType enodingFileType)
        {
            return ImportFromFiles<DBXMLBankStatement>(fileName, enodingFileType, ParseDeutscheBankStatements);
        }

        public static List<DBXMLBankStatement> ParseDeutscheBankStatements(ImportFile fileName, EncodingFileType encodingFileType)
        {
            List<DBXMLBankStatement> bsList = new List<DBXMLBankStatement>();
            try
            {
                using (XmlTextReader sr = new XmlTextReader(fileName.Dirs))
                {
                    DBXMLBankStatement bs = null;
                    while (sr.Read())
                    {
                        if (sr.NodeType == XmlNodeType.Element && sr.Name == "WYCIAG" && sr.HasAttributes)
                        {
                            bs = new DBXMLBankStatement();
                            bs.SetImportFileName(fileName);
                            bs.ReadHeader(sr);
                        }
                        if (sr.NodeType == XmlNodeType.Element && sr.Name == "OPERACJA")
                        {
                            if (bs == null)
                                bs = new DBXMLBankStatement();
                            bs.ReadBlocks(sr);
                        }
                        if (sr.NodeType == XmlNodeType.EndElement && sr.Name == "WYCIAG")
                            bsList.Add(bs);

                    }
                }
            }
            catch (Exception e)
            { }
            return bsList;
        }
        public static List<BankStatement> ImportPKOXML(ImportFile fileName, EncodingFileType enodingFileType)
        {
            return ImportFromFiles<PKOXMLBankStatement>(fileName, enodingFileType, ParsePKO);
        }

        public static List<PKOXMLBankStatement> ParsePKO(ImportFile fileName, EncodingFileType encodingFileType)
        {
            List<PKOXMLBankStatement> bsList = new List<PKOXMLBankStatement>();
            try
            {
                using (XmlTextReader sr = new XmlTextReader(fileName.Dirs))
                {
                    PKOXMLBankStatement bs = null;
                    while (sr.Read())
                    {
                        if (sr.NodeType == XmlNodeType.Element && sr.Name == "account-history")
                        {
                            bs = new PKOXMLBankStatement();
                            bs.SetImportFileName(fileName);
                            bs.ReadHeader(sr);
                        }
                        if (sr.NodeType == XmlNodeType.Element && sr.Name == "operation")
                        {
                            if (bs == null)
                                bs = new PKOXMLBankStatement();
                            bs.ReadBlocks(sr);
                        }
                        if (sr.NodeType == XmlNodeType.EndElement && sr.Name == "account-history")
                            bsList.Add(bs);

                    }
                }

            }
            catch (Exception e)
            { }
            return bsList;
        }
        
        private static List<BankStatement> ImportFromFiles<T>(ImportFile fileName, EncodingFileType encoding, Condition condition) where T : IBankStatementParser, new()
        {
            List<BankStatement> bsList = new List<BankStatement>();
            List<T> bsMT940List = ImportFromFilesMT940<T>(fileName, encoding, condition);
            foreach (T bsMT940 in bsMT940List)
            {
                BankStatement bs = bsMT940.GetBankStatement();
                bs.FileName = fileName.GetFileName();
                bsList.Add(bs);
            }
            return bsList;
        }

        private delegate List<T> ImportFromFilesMT940Delegate<T>(ImportFile fileName, EncodingFileType encodingFileType)
            where T : IBankStatementParser, new();

        private static List<BankStatement> ImportFromFiles<T>(ImportFile fileName, EncodingFileType encoding, ImportFromFilesMT940Delegate<T> importDelegate) where T : IBankStatementParser, new()
        {
            List<BankStatement> bsList = new List<BankStatement>();
            List<T> bsMT940List = importDelegate(fileName, encoding);
            foreach (T bsMT940 in bsMT940List)
            {
                BankStatement bs = bsMT940.GetBankStatement();
                bsList.Add(bs);
            }
            return bsList;
        }

        public static List<BankStatement> ImportParibasMT940(ImportFile fileName, EncodingFileType encodingFileType)
        {
            return ImportFromFiles<ParibasMT940BankStatement>(fileName, encodingFileType, delegate(string line)
            {
                return line.Length > 4 && line.Substring(0, 4) == ":20:" && line != ":20:STARTDISP";
            });
        }
    }
}



