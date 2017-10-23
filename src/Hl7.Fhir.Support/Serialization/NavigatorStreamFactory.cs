﻿/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;

#if NET_FILESYSTEM

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Factory delegate for creating a new <see cref="INavigatorStream"/> instance for a
    /// resource on disk, independent of the underlying resource serialization format.
    /// </summary>
    public delegate INavigatorStream NavigatorStreamFactory(string path);

    /// <summary>Provides a default implementation for the <see cref="NavigatorStreamFactory"/> delegate.</summary>
    /// <remarks>Supports FHIR resource files with ".xml" and ".json" extensions.</remarks>
    public static class DefaultNavigatorStreamFactory
    {
        /// <summary>
        /// Creates a new <see cref="INavigatorStream"/> instance to access the contents of a
        /// resource on disk, independent of the underlying resource serialization format.
        /// </summary>
        /// <param name="path">File path specification of a FHIR resource file.</param>
        /// <returns>A new <see cref="INavigatorStream"/> instance, or <c>null</c> (unsupported file extension).</returns>
        /// <remarks>Supports FHIR resource files with ".xml" and ".json" extensions.</remarks>
        public static INavigatorStream Create(string path)
        {
            if (FileFormats.HasXmlExtension(path))
            {
                return new XmlNavigatorStream(path);
            }
            if (FileFormats.HasJsonExtension(path))
            {
                return new JsonNavigatorStream(path);
            }

            // Unsupported extension
            return null;
        }
    }
}

#endif