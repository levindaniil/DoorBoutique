using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DoorBoutique
{
    //класс для сохранения истории изменений
    class ChangesHistory
    {
        const string ChangesFilePath = "changes.txt";
        public void SaveHistory(string change)
        {
            using (StreamWriter sw = new StreamWriter(ChangesFilePath, true, Encoding.UTF8))
            {
                sw.WriteLine($"{DateTime.Now} - {change}");
            }
        }
    }
}
