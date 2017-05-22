using System.Collections.Generic;
using System.Linq;
using JRS.DR.Exceptions;
using JRS.DR.WsModels;

namespace JRS.DR.Validators
{
    public class ApiServiceValidator
    {
        public void ValidateGetObjectiveTypes(GetObjectiveTypesRequest input)
        {
            var errors = new List<string>();

            if (!input.LanguageId.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.LanguageId)}");

            if (errors.Any())
                throw new ApiException(errors);
        }

        public void ValidateGetObjective(GetObjectiveRequest input)
        {
            var errors = new List<string>();

            if (!input.ObjectiveId.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.ObjectiveId)}");

            if (errors.Any())
                throw new ApiException(errors);
        }

        public void ValidateGetObjectives(GetObjectivesRequest input)
        {
            var errors = new List<string>();

            if (!input.LanguageId.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.LanguageId)}");

            if (!input.XLeftTop.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.XLeftTop)}");

            if (!input.YLeftTop.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.YLeftTop)}");

            if (!input.XRightBottom.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.XRightBottom)}");

            if (!input.YRightBottom.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.YRightBottom)}");

            if (errors.Any())
                throw new ApiException(errors);
        }

        public void ValidateGetObjectiveHtml(GetObjectiveHtmlRequest input)
        {
            var errors = new List<string>();

            if (!input.ObjectiveId.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.ObjectiveId)}");

            if (errors.Any())
                throw new ApiException(errors);
        }

        public void ValidateCreateObjective(CreateObjectiveRequest input)
        {
            var errors = new List<string>();

            if (!input.LanguageId.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.LanguageId)}");

            if (!input.ObjectiveTypeId.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.ObjectiveTypeId)}");

            if (string.IsNullOrEmpty(input.Name))
                errors.Add($"Param not specified. Param name: {nameof(input.Name)}");

            if (string.IsNullOrEmpty(input.Description))
                errors.Add($"Param not specified. Param name: {nameof(input.Description)}");

            if (string.IsNullOrEmpty(input.Html))
                errors.Add($"Param not specified. Param name: {nameof(input.Html)}");

            if (input.Location == null)
            {
                errors.Add($"Param not specified. Param name: {nameof(input.Location)}");
            }
            else
            {
                if (!input.Location.X.HasValue)
                    errors.Add($"Param not specified. Param name: {nameof(input.Location.X)}");

                if (!input.Location.Y.HasValue)
                    errors.Add($"Param not specified. Param name: {nameof(input.Location.Y)}");
            }

            if (errors.Any())
                throw new ApiException(errors);
        }

        public void ValidateEditObjective(EditObjectiveRequest input)
        {
            var errors = new List<string>();

            if (!input.ObjectiveId.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.ObjectiveId)}");

            if (!input.LanguageId.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.LanguageId)}");

            if (!input.ObjectiveTypeId.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.ObjectiveTypeId)}");

            if (string.IsNullOrEmpty(input.Name))
                errors.Add($"Param not specified. Param name: {nameof(input.Name)}");

            if (string.IsNullOrEmpty(input.Description))
                errors.Add($"Param not specified. Param name: {nameof(input.Description)}");

            if (string.IsNullOrEmpty(input.Html))
                errors.Add($"Param not specified. Param name: {nameof(input.Html)}");

            if (input.Location == null)
            {
                errors.Add($"Param not specified. Param name: {nameof(input.Location)}");
            }
            else
            {
                if (!input.Location.X.HasValue)
                    errors.Add($"Param not specified. Param name: {nameof(input.Location.X)}");

                if (!input.Location.Y.HasValue)
                    errors.Add($"Param not specified. Param name: {nameof(input.Location.Y)}");
            }

            if (errors.Any())
                throw new ApiException(errors);
        }

        public void ValidateDeleteObjective(DeleteObjectiveRequest input)
        {
            var errors = new List<string>();

            if (!input.ObjectiveId.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.ObjectiveId)}");

            if (errors.Any())
                throw new ApiException(errors);
        }
    }
}