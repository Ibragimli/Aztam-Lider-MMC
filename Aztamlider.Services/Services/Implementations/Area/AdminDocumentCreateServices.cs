using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area
{
    public class AdminDocumentCreateServices : IAdminDocumentCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminDocumentCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<Document> CreateDocument(DocumentCreateDto DocumentCreateDto)
        {
            await DtoCheck(DocumentCreateDto);
            var document = _mapper.Map<Document>(DocumentCreateDto);
            document.Image = _manageImageHelper.FileSave(DocumentCreateDto.ImageFile, "documents");
            document.PDF = _manageImageHelper.FileSave(DocumentCreateDto.PDFFile, "documents");
            await _unitOfWork.DocumentRepository.InsertAsync(document);
            await _unitOfWork.CommitAsync();

            return document;
        }

        private async Task DtoCheck(DocumentCreateDto documentCreateDto)
        {
            if (documentCreateDto.Name?.Length < 3)
            {
                throw new ValueFormatExpception("Layihə adının uzunluğu minimum 3 ola bilər");
            }
            if (documentCreateDto.Name?.Length > 100)
            {
                throw new ValueFormatExpception("Layihə adının uzunluğu maksimum 100 ola bilər");
            }
            if (documentCreateDto.ImageFile == null)
            {
                throw new ItemNullException("Şəkil əlavə edin!");
            }

            if (documentCreateDto.PDFFile == null)
            {
                throw new ItemNullException("PDF əlavə edin!");
            }
            if (documentCreateDto.PDFFile != null)
            {
                if (documentCreateDto.PDFFile?.Length > 3 * 1048576)
                    throw new ImageFormatException("Pdf-in max yaddaşı 3MB ola bilər!");
                if (documentCreateDto.PDFFile?.ContentType != "application/pdf")
                    throw new ItemNullException("Yalniz pdf formatda fayl əlavə edə bilərsiniz!");
            }
            if (documentCreateDto.ImageFile != null)
                _manageImageHelper.PosterCheck(documentCreateDto.ImageFile);
        }
    }
}
