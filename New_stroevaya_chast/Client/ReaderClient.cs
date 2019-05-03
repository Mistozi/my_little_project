
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModal
{
    public class ReaderClient
    {

    }
    public class TreeClass
    {
        int key_ = 0; //Ключ для поиска параграфа
        int paragraph_ = 0; // Номер паракрафа (название)
        int level_text = 0; // Уровень текста 1 .. 3
        string option_ = "."; // Краткое описание параграфа
        string types_ = "."; //Тип параграфа

        public int Key
        {
            get { return key_; }
            set
            {
                if (key_ == value)
                    return;
                key_ = value;
            }
        }
        public int Paragraph
        {
            get { return paragraph_; }
            set
            {
                if (paragraph_ == value)
                    return;
                paragraph_ = value;
            }
        }
        public int Level_text
        {
            get { return level_text; }
            set
            {
                if (level_text == value)
                    return;
                level_text = value;
            }
        }
        public string Option
        {
            get { return option_; }
            set
            {
                if (option_ == value)
                    return;
                option_ = value;
            }
        }
        public string Types
        {
            get { return types_; }
            set
            {
                if (types_ == value)
                    return;
                types_ = value;
            }
        }
    }
}
