using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Fruitylator.ViewModels;

namespace Fruitylator
{
    public class FruitylatorBootstrapper : BootstrapperBase
    {
        private readonly CompositionContainer _container;

        public FruitylatorBootstrapper()
        {
            Initialize();
            _container = new CompositionContainer(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            _container.ComposeExportedValue<IWindowManager>(new WindowManager());
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
