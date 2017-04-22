using System;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Fruitylator.Core.Interfaces;

namespace Fruitylator.ViewModels
{
    [Export(typeof(EditorViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EditorViewModel : Screen
    {
        private ITranslatableFile _translatable;

        public ITranslatableFile Translatable
        {
            get { return _translatable; }
            set
            {
                _translatable = value;
                NotifyOfPropertyChange();
            }
        }

        public void LoadTranslatable(ITranslatableFile translatable)
        {
            if (translatable == null) throw new ArgumentNullException(nameof(translatable));

            DisplayName = translatable.FilePath.Split('\\').Last();
            Translatable = translatable;
        }
    }
}