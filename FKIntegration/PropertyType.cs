using System;
using MXDokFK;

namespace FKIntegration
{
    public class PropertyType
    {
        private PropertyTypeEnum _propertyTypeEnum;

        internal PropertyType(IFirmaInfo firmaInfo)
        {
            _propertyTypeEnum = (PropertyTypeEnum)firmaInfo.TypDzialosci;
        }

        internal short Value
        {
            get { return (short)_propertyTypeEnum; }
        }

        public bool Trade
        {
            get { return (PropertyTypeEnum.Trade & _propertyTypeEnum) == PropertyTypeEnum.Trade; }
            set
            {
                if (value)
                    _propertyTypeEnum |= PropertyTypeEnum.Trade;
                else
                    _propertyTypeEnum -= PropertyTypeEnum.Trade;
            }
        }

        public bool Production
        {
            get { return (PropertyTypeEnum.Production & _propertyTypeEnum) == PropertyTypeEnum.Production; }
            set
            {
                if (value)
                    _propertyTypeEnum |= PropertyTypeEnum.Production;
                else
                    _propertyTypeEnum = _propertyTypeEnum & ~PropertyTypeEnum.Production;
            }
        }

        public bool Service
        {
            get { return (PropertyTypeEnum.Service & _propertyTypeEnum) == PropertyTypeEnum.Service; }
            set
            {
                if (value)
                    _propertyTypeEnum |= PropertyTypeEnum.Service;
                else
                    _propertyTypeEnum -= PropertyTypeEnum.Service;
            }
        }

        [Flags]
        internal enum PropertyTypeEnum
        {
            Trade = 1,
            Production = 2,
            Service = 4
        }

        public void Fill(FirmaInfo firmaInfo)
        {
            firmaInfo.TypDzialosci = Value;
        }
    }
}