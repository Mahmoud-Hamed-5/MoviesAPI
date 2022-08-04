using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Validations
{
    public class ContentTypeValidator : ValidationAttribute
    {
        private readonly string[] validContentTypes;
        private readonly string[] imageContentTypes = new string[] {"image/jpeg", "image/png", "image/gif", "image/tff" };

        public ContentTypeValidator(string[] validContentTypes)
        {
            this.validContentTypes = validContentTypes;
        }

        public ContentTypeValidator(ContentTypeGroup contentTypeGroup)
        {
            switch (contentTypeGroup)
            {
                case ContentTypeGroup.Image:
                    this.validContentTypes = imageContentTypes;
                    break;
            }               
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (!validContentTypes.Contains(formFile.ContentType))
            {
                return new ValidationResult($"Content-Type should be on of the following: {string.Join(", ", validContentTypes)}");
            }

            return ValidationResult.Success;
        }

    }


    public enum ContentTypeGroup
    {
        Image
    }
}
