using System;
using System.ComponentModel.Design;
using System.Globalization;
using CodeAnalyzer;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace RefactoringPlugin
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class RefactoringCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int cmdDelDocComments = 0x0105;
        public const int cmdDelProjComments = 0x0106;
        public const int cmdDelSolutionComments = 0x0107;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("ce166c17-df08-4f2d-be50-8b0794dd7370");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="RefactoringCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private RefactoringCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                commandService.AddCommand(new MenuCommand(new EventHandler(DeleteDocumentComments), new CommandID(CommandSet, cmdDelDocComments)));
                commandService.AddCommand(new MenuCommand(new EventHandler(SubItemCallback), new CommandID(CommandSet, cmdDelProjComments)));
                commandService.AddCommand(new MenuCommand(new EventHandler(SubItemCallback), new CommandID(CommandSet, cmdDelSolutionComments)));
            }
        }

        private void DeleteDocumentComments(object sender, EventArgs e)
        {
            var dte = (DTE)this.ServiceProvider.GetService(typeof(DTE));
            var document = dte.ActiveDocument;
            var textDocument = (TextDocument)document.Object("TextDocument");

            var startPoint = textDocument.StartPoint.CreateEditPoint();
            var endPoint = textDocument.EndPoint.CreateEditPoint();

            var editText = CodeWorker.GetTextWithoutAutoComments(startPoint.GetText(endPoint));

            if (editText != String.Empty)
                startPoint.ReplaceText(endPoint, editText, 1);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static RefactoringCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new RefactoringCommand(package);
        }

        private void SubItemCallback(object sender, EventArgs e)
        {
            IVsUIShell uiShell = (IVsUIShell)this.ServiceProvider.GetService(
                typeof(SVsUIShell));
            Guid clsid = Guid.Empty;
            int result;
            uiShell.ShowMessageBox(
                0,
                ref clsid,
                "TestCommand",
                string.Format(CultureInfo.CurrentCulture,
                "Inside TestCommand.SubItemCallback()",
                this.ToString()),
                string.Empty,
                0,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
                OLEMSGICON.OLEMSGICON_INFO,
                0,
                out result);
        }
    }
}
