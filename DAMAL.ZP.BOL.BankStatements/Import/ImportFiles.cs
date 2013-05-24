using System;
using System.IO;
using System.Windows.Forms;

namespace DAMAL.ZP.BOL.BankStatements.Import
{
    /// <summary>
    /// Klasa reprezentuje plik do importu
    /// </summary>
    public class ImportFile
    {        
        private string m_Dirs;
        private string m_ArchivePath;


        public ImportFile(string dirs, string archivePath)
        {
            m_Dirs = dirs;
            m_ArchivePath = archivePath;
        }

        public string Dirs
        {
            get { return m_Dirs; }
            set { m_Dirs = value; }
        }

        public string  ArchivePath
        {
            get { return m_ArchivePath; }
            set { m_ArchivePath = value; }
        }

        public string GetFileName()
        {
            return Path.GetFileName(Dirs);
        }


        public void MoveFileToArchive()
        {
            try
            {
                if (!Directory.Exists(m_ArchivePath))
                    Directory.CreateDirectory(m_ArchivePath);


                string fdest = Path.Combine(m_ArchivePath, Path.GetFileName(m_Dirs));

                if (fdest == m_Dirs)
                    return;
                File.Copy(m_Dirs, fdest, true);
                File.Delete(m_Dirs);
            }
            catch (Exception)
            {

                MessageBox.Show("Nie udało się przenieść pliku do archiwum", "DAMAL", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
 
            }
        }
    }
}