﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SafraEmprestimo.Domain._Base;

namespace SafraEmprestimo.Web.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            bool isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

            if (isAjaxCall)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = context.Exception is ExcecaoDeDominio ? 502 : 500;
                context.Result = context.Exception is ExcecaoDeDominio dominio ?
                    new JsonResult(dominio.MensagensDeErro) :
                    new JsonResult("An error ocorred");
                context.ExceptionHandled = true;
            }

            base.OnException(context);
        }
    }
}
