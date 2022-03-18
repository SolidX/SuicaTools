using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;

namespace Solidus.SuicaTools.Localization

{
    internal class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private ResourceManager _resourceManager;
        private readonly string _resourceKey;

        public CultureInfo? Culture { get; set; }

        public LocalizedDescriptionAttribute([NotNull]string resourceKey, Type resourceType)
        {
            _resourceManager = new ResourceManager(resourceType);
            _resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                var description = _resourceManager.GetString(_resourceKey, Culture);
                return String.IsNullOrWhiteSpace(description) ? _resourceKey : description;
            }
        }
    }
}
