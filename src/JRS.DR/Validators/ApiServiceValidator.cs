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

            if (input.Objective == null)
            {
                errors.Add($"Param not specified. Param name: {nameof(input.Objective)}");
            }
            else
            {
                if (!input.Objective.LanguageId.HasValue)
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.LanguageId)}");

                if (!input.Objective.ObjectiveTypeId.HasValue)
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.ObjectiveTypeId)}");

                if (string.IsNullOrEmpty(input.Objective.Name))
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.Name)}");

                if (string.IsNullOrEmpty(input.Objective.Description))
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.Description)}");

                if (string.IsNullOrEmpty(input.Objective.Html))
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.Html)}");

                if (input.Objective.Location == null)
                {
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.Location)}");
                }
                else
                {
                    if (!input.Objective.Location.X.HasValue)
                        errors.Add($"Param not specified. Param name: {nameof(input.Objective.Location.X)}");

                    if (!input.Objective.Location.Y.HasValue)
                        errors.Add($"Param not specified. Param name: {nameof(input.Objective.Location.Y)}");
                }
            }

            if (errors.Any())
                throw new ApiException(errors);
        }

        public void ValidateEditObjective(EditObjectiveRequest input)
        {
            var errors = new List<string>();

            if (input.Objective == null)
            {
                errors.Add($"Param not specified. Param name: {nameof(input.Objective)}");
            }
            else
            {
                if (!input.Objective.ObjectiveId.HasValue)
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.ObjectiveId)}");

                if (!input.Objective.LanguageId.HasValue)
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.LanguageId)}");

                if (!input.Objective.ObjectiveTypeId.HasValue)
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.ObjectiveTypeId)}");

                if (string.IsNullOrEmpty(input.Objective.Name))
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.Name)}");

                if (string.IsNullOrEmpty(input.Objective.Description))
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.Description)}");

                if (string.IsNullOrEmpty(input.Objective.Html))
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.Html)}");

                if (input.Objective.Location == null)
                {
                    errors.Add($"Param not specified. Param name: {nameof(input.Objective.Location)}");
                }
                else
                {
                    if (!input.Objective.Location.X.HasValue)
                        errors.Add($"Param not specified. Param name: {nameof(input.Objective.Location.X)}");

                    if (!input.Objective.Location.Y.HasValue)
                        errors.Add($"Param not specified. Param name: {nameof(input.Objective.Location.Y)}");
                }
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