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
            _container.ComposeExportedValue<Func<SolutionItem, BrowserViewModel>>(solutionItem =>
            {
                var editor = IoC.Get<BrowserViewModel>();
                editor.LoadSolution(solutionItem);
                return editor;
            });
            _container.ComposeExportedValue<Func<ITranslatablePart, SearchDetailsViewModel>>(tlPart =>
            {
                var editor = IoC.Get<SearchDetailsViewModel>();
                editor.LoadTranslatablePart(tlPart);
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
