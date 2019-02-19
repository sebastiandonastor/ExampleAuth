using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ExampleAuth.Controllers
{
    internal static class TokenGenerator
    {
        public static String GenerateTokenJwt(string username)
        {

            // Llave secreta para encriptar
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];

            // Quien o quienes se alimentaran de nuestro frontend
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];

            // Quien auditara la api
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];

            // Tiempo de expiracion del token
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_TIME"];


            // Encryptando la secret Key
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));

            // Poblando el verify Signature con la llave secreta para la comparacion
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Que devolvera el payload
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });


            // Inicializando el objeto que se encargara de crear el token
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();


            // Estructura el token
            var jwtTokenStructure = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials
                );


            //Escribe el codigo y convirtelo en un string
            var jwtFormatted = tokenHandler.WriteToken(jwtTokenStructure);

            return jwtFormatted;
        }
    }
}
