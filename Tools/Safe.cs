﻿using System;
using System.Linq.Expressions;
using UnityEngine;

namespace Yun.Tools {
    public static class Safe {

        public static Action<Exception> Error;

        public static bool Call(Expression<Action> callExp) {
            var methodCallExp = (MethodCallExpression)callExp.Body;
            var methodName = methodCallExp.Method.Name;
            var classname = methodCallExp.Object.Type.Name;

            var call = callExp.Compile();
            try {
                call();
            } catch(Exception exc) {
#if UNITY_EDITOR
                Debug.LogError(
                    string.Format("An Exception occured in 'Yun.{0}.{1}()'", classname, methodName));
                Debug.Log(exc);
#endif

                if(Error != null)
                    Error(exc); 
                return false;
            }

            return true;
        }

    }
}
