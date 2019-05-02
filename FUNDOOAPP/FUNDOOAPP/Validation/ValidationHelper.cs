//-----------------------------------------------------------------------
// <copyright file="ValidationHelper.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Xamarin.Forms;

    /// <summary>
    /// this ValidationHelper instance
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Determines whether [is form valid] [the specified model].
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        ///   <c>true</c> if [is form valid] [the specified model]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFormValid(object model, Page page)
        {
            HideValidationFields(model, page);
            var errors = new List<ValidationResult>();
            var context = new ValidationContext(model);
            bool isValid = Validator.TryValidateObject(model, context, errors, true);
            if (!isValid)
            {
                ShowValidationFields(errors, model, page);
            }

            return errors.Count() == 0;
        }

        /// <summary>
        /// Hides the validation fields.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="page">The page.</param>
        /// <param name="validationLabelSuffix">The validation label suffix.</param>
        private static void HideValidationFields(object model, Page page, string validationLabelSuffix = "Error")
        {
            if (model == null)
            {
                return;
            }

            var properties = GetValidatablePropertyNames(model);
            foreach (var propertyName in properties)
            {
                var errorControlName =
                $"{propertyName.Replace(".", "_")}{validationLabelSuffix}";
                var control = page.FindByName<Label>(errorControlName);
                if (control != null)
                {
                    control.IsVisible = false;
                }
            }
        }

        /// <summary>
        /// Shows the validation fields.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <param name="model">The model.</param>
        /// <param name="page">The page.</param>
        /// <param name="validationLabelSuffix">The validation label suffix.</param>
        private static void ShowValidationFields(List<ValidationResult> errors, object model, Page page, string validationLabelSuffix = "Error")
        {
            if (model == null)
            {
                return;
            }

            foreach (var error in errors)
            {
                var memberName = $"{model.GetType().Name}_{error.MemberNames.FirstOrDefault()}";
                memberName = memberName.Replace(".", "_");
                var errorControlName = $"{memberName}{validationLabelSuffix}";
                var control = page.FindByName<Label>(errorControlName);
                if (control != null)
                {
                    control.Text = $"{error.ErrorMessage}{Environment.NewLine}";
                    control.IsVisible = true;
                }
            }
        }

        /// <summary>
        /// Gets the valid property names.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return task</returns>
        private static IEnumerable<string> GetValidatablePropertyNames(object model)
        {
            var validatableProperties = new List<string>();
            var properties = GetValidatableProperties(model);
            foreach (var propertyInfo in properties)
            {
                var errorControlName = $"{propertyInfo.DeclaringType.Name}.{propertyInfo.Name}";
                validatableProperties.Add(errorControlName);
            }

            return validatableProperties;
        }

        /// <summary>
        /// Gets the valid properties.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return task</returns>
        private static List<PropertyInfo> GetValidatableProperties(object model)
        {
            var properties = model.GetType().GetProperties().Where(prop => prop.CanRead
                && prop.GetCustomAttributes(typeof(ValidationAttribute), true).Any()
                && prop.GetIndexParameters().Length == 0).ToList();
            return properties;
        }
    }
}
