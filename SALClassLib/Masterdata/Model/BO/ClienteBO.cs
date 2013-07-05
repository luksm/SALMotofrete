﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SALClassLib.Masterdata.Model.DAO;
using NHibernate;
using Utilitarios.BO;

namespace SALClassLib.Masterdata.Model.BO
{
    
    public class ClienteBO : BO<Cliente>
    {
        private ISession sessao = null;
        private ClienteDAO dao = null;

        public ClienteBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            dao = new ClienteDAO(sessao);
            base.Dao = dao;
        }

        ~ClienteBO()
        {
            Dispose();
        }

        public new void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }
    }
        
}
