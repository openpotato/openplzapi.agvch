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

using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// Extensions for <see cref="IDictionary<string, string>"/>
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds an element with provided key and value to the dictionary, but only if value is not empty. 
        /// </summary>
        /// <param name="dic">A <see cref="IDictionary<string, string>"/> instance</param>
        /// <param name="key">Parameter key</param>
        /// <param name="value">Parameter value</param>
        /// <returns>Updated instance of the <see cref="UriBuIDictionary<string, string>ilder"/></returns>
        public static void AddIfNotEmpty(this IDictionary<string, string> dic, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                dic.Add(key, value);
            }
        }
    }
}