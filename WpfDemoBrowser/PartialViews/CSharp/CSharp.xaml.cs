using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace CS
{
    /// <summary>
    /// C12.xaml 的交互逻辑
    /// </summary>
    public partial class CSharp : UserControl
    {
        static ResourceDictionary resourceDictionary;
        static FlowDocument doc;

        public CSharp()
        {
            InitializeComponent();

            doc = docViewer.Document;
            resourceDictionary = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/WpfDemoBrowser;component/Resources/Styles.xaml")
            };

            SetDoc();
        }

        public static void AddTitle(string text)
        {
            // 一级标题
            doc.Blocks.Add(new Paragraph(new Run(text))
            {
                Style = resourceDictionary["Heading"] as Style
            });
        }

        public static void AddSubtitle(string text)
        {
            // 二级标题
            doc.Blocks.Add(new Paragraph(new Run(text))
            {
                Style = resourceDictionary["Subheading"] as Style
            });
        }

        public static void AddParagraph(string text)
        {
            // 段落
            doc.Blocks.Add(new Paragraph(new Run(text)));
        }

        public static void AddCSCode(string code, string codeStyle=null)
        {
            
            TextEditor textEditor = new TextEditor
            {
                Text = code
            };
            if (codeStyle != null)
                textEditor.Style = resourceDictionary[codeStyle] as Style;
            doc.Blocks.Add(new BlockUIContainer(textEditor));
        }

        public static void AddList(List<string> textList, TextMarkerStyle markerStyle = TextMarkerStyle.Disc)
        {
            List list = new List
            {
                MarkerStyle = markerStyle
            };
            foreach (var text in textList)
            {
                list.ListItems.Add(new ListItem(new Paragraph(new Run(text))));
            }
            doc.Blocks.Add(list);
        }

        public void SetDoc()
        {
            using (StreamReader sr = new StreamReader(@"E:\HelloCSharp\WpfDemoBrowser\WpfDemoBrowser\DocumentCode\C1.md"))
            {
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    if (Regex.IsMatch(str, @"^# "))
                    {
                        string title = Regex.Replace(str, @"^# (?<title>.*?)$", "${title}");
                        AddTitle(title);
                    }
                    else if (Regex.IsMatch(str, @"^## "))
                    {
                        string title = Regex.Replace(str, @"^## (?<title>.*?)$", "${title}");
                        AddSubtitle(title);
                    }
                    else if (Regex.IsMatch(str, @"^```"))
                    {
                        string start = str;

                        string code = "";
                        while ((str = sr.ReadLine()) != null)
                        {
                            if (Regex.IsMatch(str, @"^```$"))
                            {
                                break;
                            }
                            if (Regex.IsMatch(str, @"^\\\[.*?\]$"))
                            {
                                string name = Regex.Replace(str, @"^\\\[(?<name>.*?)\]$", "${name}");
                                code +=name;
                            }
                            else
                            {
                                if (code.Length == 0)
                                {
                                    code += str;
                                }
                                else
                                {
                                    code += str + Environment.NewLine;
                                }
                            }
                        }
                        string st = Regex.Replace(start, @"^```(?<name>.*?)$", "${name}");
                        if (st == "C#")
                        {
                            AddCSCode(code, "CSCode");
                        }
                        else if(st=="Xml")
                        {
                            AddCSCode(code, "XmlCode");
                        }
                        else if (st == "Json")
                        {
                            AddCSCode(code, "JsonCode");
                        }
                        else
                        {
                            AddCSCode(code, "TxtCode");
                        }
                    }
                    else if (Regex.IsMatch(str, @"^[0-9]+\. "))
                    {
                        List<string> textList = new List<string>();
                        do
                        {
                            if (!Regex.IsMatch(str, @"^[0-9]+\. "))
                            {
                                break;
                            }
                            string text = Regex.Replace(str, @"^[0-9]+\. (?<text>.*?)$", "${text}");
                            textList.Add(text);
                        }
                        while ((str = sr.ReadLine()) != null);
                        AddList(textList, TextMarkerStyle.Decimal);
                    }
                    else if (Regex.IsMatch(str, @"^\*+ "))
                    {
                        List<string> textList = new List<string>();
                        do
                        {
                            if (!Regex.IsMatch(str, @"^\*+ "))
                            {
                                break;
                            }
                            string text = Regex.Replace(str, @"^\*+ (?<text>.*?)$", "${text}");
                            textList.Add(text);
                        }
                        while ((str = sr.ReadLine()) != null);
                        AddList(textList, TextMarkerStyle.Disc);
                    }
                    else
                    {
                        AddParagraph(str);
                    }
                }
            }

            //AddTitle("第1章");
            //AddSubtitle("1.1 Hello, World!");
            //AddParagraph("Hello, World!");
            //AddCSCode("using Hello;\nnamespace {\n}");
            //AddList(new string[]
            //{
            //    "白色", "黑色"
            //});
        }
    }
}
