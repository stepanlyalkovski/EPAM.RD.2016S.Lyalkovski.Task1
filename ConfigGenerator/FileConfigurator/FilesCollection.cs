﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigGenerator.FileConfigurator
{
    [ConfigurationCollection(typeof(FileElement))]
    public class FilesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FileElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FileElement)element).Path;
        }

        public FileElement this[int idx] => (FileElement)BaseGet(idx);
    }
}