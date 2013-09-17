using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Models
{
        /// <summary>
        /// Dette er en abstrakt klasse som brukes til serialisering / de-serialisering til XML
        /// </summary>
        public abstract class XmlSerializable
        {
            #region //****** Member Variables ******//
            private string m_FileName = string.Empty;
            private string FileName
            {
                get { return m_FileName; }
                set { m_FileName = value; }
            }
            #endregion //****** Member Variables ******//

            #region //****** Constructors ******//
            public XmlSerializable()
            {
            }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="FileName"></param>
            public XmlSerializable(string FileName)
            {
                m_FileName = FileName;
            }
            #endregion //****** Constructors ******//

            #region //****** Overrides ******//
            #endregion //****** Overrides ******//

            #region //****** Events ******//
            #endregion //****** Events ******//

            #region //****** Properties ******//
            #endregion //****** Properties ******//

            #region //****** Methods ******//

            public virtual void Save()
            {
                if (m_FileName.Length > 0)
                    this.Save(m_FileName);
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="FileName"></param>
            /// <param name="FilePath"></param>
            public virtual void Save(string FileName)
            {
                try
                {
                    //La oss her bare være sikker på at stien eksisterer!
                    FileInfo myFile = new FileInfo(FileName);
                    Directory.CreateDirectory(myFile.DirectoryName);

                    StreamWriter w = new StreamWriter(FileName);
                    XmlSerializer s = new XmlSerializer(this.GetType());
                    s.Serialize(w, this);
                    w.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to save XML file! " + ex.Message);
                }
            }

            public virtual string XMLToString()
            {
                try
                {
                    StringWriter w = new StringWriter();
                    string retVal = string.Empty;
                    //StreamWriter w = new StreamWriter(FileName);
                    XmlSerializer s = new XmlSerializer(this.GetType());
                    s.Serialize(w, this);
                    retVal = w.ToString();
                    w.Close();
                    return retVal;
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to save XML file to string! " + ex.Message);
                }
            }

            public virtual void Load()
            {
                if (m_FileName.Length > 0)
                    this.Load(m_FileName);
            }

            public virtual void Load(string FileName)
            {
                try
                {
                    if (File.Exists(FileName))
                    {
                        StreamReader sr = new StreamReader(FileName);
                        XmlTextReader xr = new XmlTextReader(sr);
                        XmlSerializer xs = new XmlSerializer(this.GetType());
                        object c;
                        if (xs.CanDeserialize(xr))
                        {
                            c = xs.Deserialize(xr);
                            Type t = this.GetType();
                            PropertyInfo[] properties = t.GetProperties();
                            foreach (PropertyInfo p in properties)
                            {
                                p.SetValue(this, p.GetValue(c, null), null);
                            }
                        }
                        xr.Close();
                        sr.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to load XML file! " + ex.Message);
                }
            }

            public virtual void LoadFromString(string XML)
            {
                try
                {
                    StringReader sr = new StringReader(XML);
                    XmlTextReader xr = new XmlTextReader(sr);
                    XmlSerializer xs = new XmlSerializer(this.GetType());
                    object c;
                    if (xs.CanDeserialize(xr))
                    {
                        c = xs.Deserialize(xr);
                        Type t = this.GetType();
                        PropertyInfo[] properties = t.GetProperties();
                        foreach (PropertyInfo p in properties)
                        {
                            p.SetValue(this, p.GetValue(c, null), null);
                        }
                    }
                    xr.Close();
                    sr.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to load XML file from string! " + ex.Message);
                }
            }
            #endregion //****** Methods ******//
    }
}
