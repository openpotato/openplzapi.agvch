#region OpenPlzApi.AGVCH - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    OpenPlzApi.AGVCH 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System.Text;

namespace System
{
    /// <summary>
    /// Extensions for <see cref="UriBuilder"/>
    /// </summary>
    public static class UriBuilderExtensions
    {
        /// <summary>
        /// Appends a query string parameter with a key, and a value. 
        /// </summary>
        /// <param name="ub">A <see cref="UriBuilder"/> instance</param>
        /// <param name="key">Parameter key</param>
        /// <param name="value">Parameter value</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>Updated instance of the <see cref="UriBuilder"/></returns>
        public static UriBuilder WithParameter(this UriBuilder ub, string key, int value) => ub.WithParameter(key, value.ToString());

        /// <summary>
        /// Appends a query string parameter with a key if the value is TRUE
        /// </summary>
        /// <param name="ub">A <see cref="UriBuilder"/> instance</param>
        /// <param name="key">Parameter key</param>
        /// <param name="value">Parameter value</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>Updated instance of the <see cref="UriBuilder"/></returns>
        public static UriBuilder WithParameter(this UriBuilder ub, string key, bool value) => ub.WithParameter(key, value.ToString());

        /// <summary>
        /// Appends a query string parameter with a key, and a value. 
        /// </summary>
        /// <param name="ub">A <see cref="UriBuilder"/> instance</param>
        /// <param name="key">Parameter key</param>
        /// <param name="value">Parameter value</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>Updated instance of the <see cref="UriBuilder"/></returns>
        public static UriBuilder WithParameter(this UriBuilder ub, string key, string value)
        {
            if (ub == null)
            {
                throw new ArgumentNullException(nameof(ub));
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (!string.IsNullOrWhiteSpace(value))
            {
                var sb = new StringBuilder();

                sb.Append(string.IsNullOrWhiteSpace(ub.Query) ? "" : $"{ub.Query.TrimStart('?')}&");
                sb.Append(Uri.EscapeDataString(key));
                sb.Append('=');
                sb.Append(Uri.EscapeDataString(value));

                ub.Query = sb.ToString();
            }
            return ub;
        }

        /// <summary>
        /// Appends a relative path 
        /// </summary>
        /// <param name="ub">A <see cref="UriBuilder"/> instance</param>
        /// <param name="relativePath">Relative path to be appended</param>
        /// <returns>Updated instance of the <see cref="UriBuilder"/></returns>
        public static UriBuilder WithRelativePath(this UriBuilder ub, string relativePath)
        {
            ub.Path += relativePath;
            return ub;
        }
    }
}