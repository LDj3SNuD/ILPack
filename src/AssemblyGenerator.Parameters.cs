﻿using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace Lokad.ILPack
{
    public partial class AssemblyGenerator
    {
        int _nextParameterRowId = 1;

        /// <summary>
        ///     Creates parameter metadata of a method parameters.
        /// </summary>
        /// <param name="parameters">Method parameters</param>
        /// <returns>
        ///     Metadata handle of first parameter if number of parameters is greater than zero,
        ///     null metadata otherwise.
        /// </returns>
        private ParameterHandle CreateParameters(ParameterInfo[] parameters)
        {
            var firstHandle = MetadataTokens.ParameterHandle(_nextParameterRowId);
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                if (_metadata.TryGetParameterHandle(parameter, out var parameterDef))
                {
                    throw new InvalidOperationException("Duplicate emit of parameter");
                }

                parameterDef =
                    _metadata.Builder.AddParameter(parameter.Attributes, _metadata.GetOrAddString(parameter.Name), i);

                System.Diagnostics.Debug.Assert(parameterDef == MetadataTokens.ParameterHandle(_nextParameterRowId));

                _nextParameterRowId++;

                _metadata.AddParameterHandle(parameter, parameterDef);
                CreateCustomAttributes(parameterDef, parameter.GetCustomAttributesData());
            }

            return firstHandle;
        }
    }
}