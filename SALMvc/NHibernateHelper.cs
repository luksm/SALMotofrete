using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace SALMvc
{
    public sealed class NHibernateHelper
    {
        private const string CurrentSessionKey = "nhibernate.current_session";
        private static readonly ISessionFactory sessionFactory;

        static NHibernateHelper()
        {
            Configuration cfg = new Configuration();
            cfg.Configure();
            new SchemaUpdate(cfg).Execute(false, true);
            sessionFactory = cfg.BuildSessionFactory();
        }

        public static ISession GetCurrentSession()
        {
            ISession currentSession = null;

            currentSession = sessionFactory.OpenSession();

            return currentSession;
        }

        public static void CloseSession(ISession session)
        {
            session.Close();
        }

        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }
    }
}