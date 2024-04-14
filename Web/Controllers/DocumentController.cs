using Microsoft.AspNetCore.Mvc;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Application.Wrapper.Document;
using Application.Wrapper.Attachment;

namespace Web.Controllers
{
    public class DocumentController : SidebarMenuController
    {

        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;


        #endregion

        private readonly ILogger<DocumentController> _logger;
        private readonly IDocumentWrapper _documentWrapper;
        private readonly IAttachmentWrapper _attachmentWrapper;

        public DocumentController(ILogger<DocumentController> logger,
           IDocumentWrapper documentWrapper,

           Web.Utils.CookieManager cookieManager,

           IRolesMenuWrapper rolesMenuWrapper,
           IAttachmentWrapper attachmentWrapper

           ) : base(rolesMenuWrapper, cookieManager)
        {

            _logger = logger;
            _attachmentWrapper = attachmentWrapper;

            _documentWrapper = documentWrapper;


            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
            _attachmentWrapper = attachmentWrapper;


            #endregion


        }


        public ActionResult IndexForPublicDocument()
        {
            var data = _documentWrapper.GetAllDataForPublic();
            return View(data);
        }

        public ActionResult CreatePublicDocument()
        {
            _cookieManager.SetCookie("CreatePublicDocument",Guid.NewGuid().ToString());
            return View();
        }

        [HttpPost]
        public ActionResult CreatePublicDocument(DocumentModel model)
        {
            var getguid = _cookieManager.GetCookie("CreatePublicDocument");
            var getattachmentfiles = GlobalController.ListDocumentCreatePublic.Where(x => x.GUID == getguid).ToList();

            Document formmodel = new Document();
            formmodel.Documentname = model.DocumentName;
            formmodel.IsPublic = true;
            formmodel.Description = model.Description;
            _documentWrapper.CreateData(formmodel);

            //DocumentLogic.CreateData(model);

            foreach (var i in getattachmentfiles)
            {
                Attachmentfile fileModel = new Attachmentfile();
                fileModel.Documentid = formmodel.Id;
                fileModel.Filetype = i.filetype;
                fileModel.Isdocumentid = true;
                fileModel.Url = i.Path;
                _attachmentWrapper.CreateData(fileModel);
            }

            //Session["CreatePublicDocument"] = Guid.NewGuid();
            return RedirectToAction("IndexForPublicDocument");
        }

        public ActionResult EditPublicDocument(int id)
        {
            var cookieEdit = Guid.NewGuid().ToString();

            _cookieManager.SetCookie("EditPublicDocument", cookieEdit);
            var data = _documentWrapper.GetPublicDocumentById(id);

            foreach (var j in data.ListAttachmentFiles)
            {
                var getguid = cookieEdit;
                FormAttachmentFilesModel Attachfilelist = new FormAttachmentFilesModel();
                Attachfilelist.id = j.Id;
                Attachfilelist.GUID = getguid;
                Attachfilelist.Path = j.Url;
                Attachfilelist.TransactionID = id;
                Attachfilelist.filetype = j.Filetype;
                Attachfilelist.delflag = false;
                var checkdata = GlobalController.ListDocumentEditPublic.Where(x => x.id == j.Id && x.GUID == getguid).Any();
                if (checkdata == false)
                {
                    GlobalController.ListDocumentEditPublic.Add(Attachfilelist);
                }
            }

            return View(data);
        }


        [HttpPost]
        public ActionResult EditPublicDocument(DocumentModel model)
        {
            var getguid = _cookieManager.GetCookie("EditPublicDocument") != null ? _cookieManager.GetCookie("EditPublicDocument") : "";

            Document formmodel = new Document();
            formmodel.Id = model.id;
            formmodel.Documentname = model.DocumentName;
            formmodel.IsPublic = true;
            formmodel.Description = model.Description;
            _documentWrapper.Update(formmodel);


            var listdataattach = GlobalController.ListDocumentEditPublic.Where(x => x.GUID == getguid && x.delflag == false).ToList();

            var getolddataAttachment = _attachmentWrapper.GetDataByDocumentId(formmodel.Id).ToList();

            foreach (var k in getolddataAttachment)
            {
                _attachmentWrapper.Delete(k);
            }

            //var data = _attachmentWrapper.

            foreach (var i in listdataattach)
            {
                Attachmentfile AttachmentFilemodel = new Attachmentfile();
                AttachmentFilemodel.Documentid = formmodel.Id;
                AttachmentFilemodel.Filetype = i.filetype;
                AttachmentFilemodel.Isdocumentid = true;
                AttachmentFilemodel.Url = i.Path;
                _attachmentWrapper.CreateData(AttachmentFilemodel);
            }



            return RedirectToAction("IndexForPublicDocument");
        }

        public ActionResult DetailsPublicDocument(int id)
        {
            var data = _documentWrapper.GetPublicDocumentById(id);
            return View(data);
        }




        public ActionResult DeletePublicDocument(int id)
        {
            var data = _documentWrapper.GetDataById(id);
            return View(data);
        }





        public ActionResult IndexByProject()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;
            var data = _documentWrapper.GetAllDataByProject(ProjectId);
            return View(data);
        }


        public ActionResult CreateDocumentByProject()
        {

            _cookieManager.SetCookie("CreateDocumentProject",Guid.NewGuid().ToString());

            return View();
        }

        [HttpPost]
        public ActionResult CreateDocumentByProject(DocumentModel model)
        {

            var getguid = _cookieManager.GetCookie("CreateDocumentProject") != null ? _cookieManager.GetCookie("CreateDocumentProject") : "";
            var getattachmentfiles = GlobalController.ListDocumentCreateDocumentProject.Where(x => x.GUID == getguid).ToList();

            Document formmodel = new Document();
            formmodel.Documentname = model.DocumentName;
            formmodel.IsPublic = false;
            formmodel.Description = model.Description;
            formmodel.Projectid = model.ProjectId;
            _documentWrapper.CreateData(formmodel);

            //DocumentLogic.CreateData(model);

            foreach (var i in getattachmentfiles)
            {
                Attachmentfile AttachmentFilemodel = new Attachmentfile();
                AttachmentFilemodel.Documentid = formmodel.Id;
                AttachmentFilemodel.Filetype = i.filetype;
                AttachmentFilemodel.Isdocumentid = true;
                AttachmentFilemodel.Url = i.Path;
                _attachmentWrapper.CreateData(AttachmentFilemodel);
            }

            //Session["CreatePublicDocument"] = Guid.NewGuid();
            return RedirectToAction("IndexByProject");
        }



        public ActionResult EditDocumentByProject(int id)
        {
            _cookieManager.SetCookie("EditDocumentProject",Guid.NewGuid().ToString());
            //var data = DocumentLogic.GetDataById(id);
            var data = _documentWrapper.GetDocumentProjectById(id);

            foreach (var j in data.ListAttachmentFiles)
            {
                var getguid = _cookieManager.GetCookie("EditDocumentProject") != null ? _cookieManager.GetCookie("EditDocumentProject").ToString() : "";
                FormAttachmentFilesModel Attachfilelist = new FormAttachmentFilesModel();
                Attachfilelist.id = j.Id;
                Attachfilelist.GUID = getguid;
                Attachfilelist.Path = j.Url;
                Attachfilelist.TransactionID = id;
                Attachfilelist.filetype = j.Filetype;
                Attachfilelist.delflag = false;
                var checkdata = GlobalController.ListDocumentEditDocumentProject.Where(x => x.id == j.Id && x.GUID == getguid).Any();
                if (checkdata == false)
                {
                    GlobalController.ListDocumentEditDocumentProject.Add(Attachfilelist);
                }
            }

            return View(data);
            //return View(data);
        }

        [HttpPost]
        public ActionResult EditDocumentByProject(DocumentModel model)
        {
            var getguid = _cookieManager.GetCookie("EditDocumentProject") != null ? _cookieManager.GetCookie("EditDocumentProject") : "";

            Document formmodel = new Document();
            formmodel.Documentname = model.DocumentName;
            formmodel.IsPublic = false;
            formmodel.Description = model.Description;
            formmodel.Projectid = model.ProjectId;
            _documentWrapper.Update(formmodel);


            var listdataattach = GlobalController.ListDocumentEditDocumentProject.Where(x => x.GUID == getguid && x.delflag == false).ToList();

            var getolddataAttachment = _attachmentWrapper.GetDataByDocumentId(formmodel.Id).ToList();

            foreach (var k in getolddataAttachment)
            {
                _attachmentWrapper.Delete(k);
            }

            //var data = _attachmentWrapper.

            foreach (var i in listdataattach)
            {
                Attachmentfile AttachmentFilemodel = new Attachmentfile();
                AttachmentFilemodel.Documentid = formmodel.Id;
                AttachmentFilemodel.Filetype = i.filetype;
                AttachmentFilemodel.Isdocumentid = true;
                AttachmentFilemodel.Url = i.Path;
                _attachmentWrapper.CreateData(AttachmentFilemodel);
            }



            return RedirectToAction("IndexByProject");
        }




        public ActionResult DetailsDocumentByProject(int id)
        {
            var data = _documentWrapper.GetDocumentProjectById(id);
            return View(data);
        }

        public ActionResult DeleteDocumentByProject(int id)
        {
            var data = _documentWrapper.GetDocumentProjectById(id);
            return View(data);
        }


    }
}
