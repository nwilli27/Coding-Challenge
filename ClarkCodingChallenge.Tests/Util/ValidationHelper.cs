using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ClarkCodingChallenge.Tests.Util
{
	public class ValidationHelper
	{
		#region Members

		private List<ValidationResult> ValidationResults = new List<ValidationResult>();

		#endregion

		#region Properties

		public bool HasValidationErrors => this.ValidationResults.Any();

		#endregion

		#region Methods

		public void ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            ValidationResults.AddRange(validationResults);
        }

        public bool HasValidationError(string fieldName, string errorMessage) => this.ValidationResults.Any(v => v.MemberNames.Contains(fieldName) && v.ErrorMessage == errorMessage);

		#endregion
	}
}
