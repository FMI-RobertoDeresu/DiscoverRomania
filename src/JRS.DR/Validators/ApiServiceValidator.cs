using System.Collections.Generic;
using System.Linq;
using JRS.DR.Exceptions;
using JRS.DR.WsModels;

namespace JRS.DR.Validators
{
    public class ApiServiceValidator
    {
        public void ValidateGetObjectives(GetObjectivesRequest input)
        {
            var errors = new List<string>();

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

        public void ValidateGetObjective(GetObjectiveRequest input)
        {
            var errors = new List<string>();

            if (!input.ObjectiveId.HasValue)
                errors.Add($"Param not specified. Param name: {nameof(input.ObjectiveId)}");

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
    }
}