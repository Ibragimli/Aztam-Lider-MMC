using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Documents;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Documents
{
    public class AdminDocumentEditServices : IAdminDocumentEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminDocumentEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task EditDocument(Document Document)
        {
            bool checkBool = false;

            var oldDocument = await GetDocument(Document.Id);
            if (oldDocument == null)
                throw new ItemNullException("Layihə tapılmadı!");

            await Check(Document);

            if (ImageChange(Document, oldDocument) == 1)
                checkBool = true;

            if (PDFChange(Document, oldDocument) == 1)
                checkBool = true;

            if (oldDocument.Name != Document.Name)
            {
                oldDocument.Name = Document.Name;
                checkBool = true;
            }

            if (oldDocument.License != Document.License)
            {
                oldDocument.License = Document.License;
                checkBool = true;
            }
            oldDocument.ModifiedDate = DateTime.UtcNow.AddHours(4);

            if (checkBool)
                await _unitOfWork.CommitAsync();
        }

        public async Task<Document> GetDocument(int id)
        {
            var Document = await _unitOfWork.DocumentRepository.GetAsync(x => x.Id == id);
            return Document;
        }

        private int ImageChange(Document Document, Document projectExist)
        {
            if (Document.ImageFile != null)
            {
                var posterImageFile = Document.ImageFile;

                if (projectExist.Image == null) throw new ImageNullException("Şəkil tapılmadı!");

                string filename = _manageImageHelper.FileSave(posterImageFile, "documents");
                _manageImageHelper.DeleteFile(projectExist.Image, "Documents");
                projectExist.Image = filename;
                return 1;
            }
            return 0;

        }

        private int PDFChange(Document Document, Document projectExist)
        {
            if (Document.PDFFile != null)
            {
                var pdfFile = Document.PDFFile;

                if (projectExist.PDF == null) throw new ImageNullException("PDF tapılmadı!");

                string filename = _manageImageHelper.FileSave(pdfFile, "documents");
                _manageImageHelper.DeleteFile(projectExist.PDF, "Documents");
                projectExist.PDF = filename;
                return 1;
            }
            return 0;
        }

        private async Task Check(Document Document)
        {

            if (Document.Name == null)
            {
                throw new ItemNullException("Sənəd adını qeyd edin!");
            }
            if (Document.Name?.Length < 3)
            {
                throw new ValueFormatExpception("Layihə adının uzunluğu minimum 3 ola bilər");
            }
            if (Document.Name?.Length > 100)
            {
                throw new ValueFormatExpception("Layihə adının uzunluğu maksimum 100 ola bilər");
            }
          
            
            if (Document.PDFFile != null)
            {
                if (Document.PDFFile.Length > 3 * 1048576)
                    throw new ImageFormatException("Pdf-in max yaddaşı 3MB ola bilər!");
                if (Document.PDFFile.ContentType != "application/pdf")
                    throw new ItemNullException("Yalniz pdf formatda fayl əlavə edə bilərsiniz!");
            }
            if (Document.ImageFile != null)
                _manageImageHelper.PosterCheck(Document.ImageFile);

        }
    }
}
