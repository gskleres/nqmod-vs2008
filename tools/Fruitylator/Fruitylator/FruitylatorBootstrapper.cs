using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Fruitylator.Core.Interfaces;
using Fruitylator.ViewModels;

namespace Fruitylator
{
    public class FruitylatorBootstrapper : BootstrapperBase
    {
        private readonly CompositionContainer _container;

        public FruitylatorBootstrapper()
        {
            Initialize();
            var catalog = new AggregateCatalog(
                new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
            _container = new CompositionContainer(catalog);
            _container.ComposeExportedValue<IWindowManager>(new WindowManager());
            _container.ComposeExportedValue<IEventAggregator>(new EventAggregator());

            // editor factory
            _container.ComposeExportedValue<Func<ITranslatableFile, EditorViewModel>>(translatable =>
            {
                var editor = IoC.Get<EditorViewModel>();
                editor.LoadTranslatable(translatable);
                return editor;
            });
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetExportedValue<object>(service.FullName);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetExportedValues<object>(service.FullName);
        }

        protected override void BuildUp(object instance)
        {
            _container.ComposeParts(instance);
        }
    }
}
