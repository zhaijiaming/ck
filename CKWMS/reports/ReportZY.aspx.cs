﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKWMS.BSL;
using CKWMS.EFModels;
using CKWMS.BSL;
using CKWMS.IBSL;

namespace CKWMS.reports
{
    public partial class ReportZY : System.Web.UI.Page
    {
        private Iwms_jianhuoService ob_wms_jianhuoservice = ServiceFactory.wms_jianhuoservice;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int _userid = (int)Session["user_id"];
                string name = "";
                int i = 0;
                string _outid = "";
                ReportDataSetZY _rds = new ReportDataSetZY();
                //DataTable _dt;                
                if (Request.QueryString["pid"] != null)
                {
                    name = Request.QueryString["pid"];
                    
                    switch (name)
                    {
                        case "daviskw":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/daviskw.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtkw = _rds.Tables["davistest-kw"];
                            var _kws = ServiceFactory.wms_kuweiservice.LoadEntities(p => p.IsDelete == false);
                            DataRow drkw;
                            foreach (wms_kuwei _pr in _kws)
                            {
                                drkw = dtkw.NewRow();
                                drkw["kuwei"] = _pr.Mingcheng;
                                drkw["huojia"] = _pr.Huojia;
                                drkw["lei"] = _pr.Lieshu;
                                drkw["ceng"] = _pr.Censhu;
                                dtkw.Rows.Add(drkw);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["davistest-kw"]));
                            break;
                        case "JianHuoDan":
                            rptView.Reset();
                            _outid = Request.QueryString["out"] ?? "";
                            rptView.LocalReport.ReportPath = "reports/rptJianHuoDan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtjhd = _rds.Tables["JianHuoDan"];
                            var _jhds = ob_wms_jianhuoservice.GetPickDetail(int.Parse(_outid), p => p.DaijianSL > 0);
                            DataRow drjhd;
                            foreach (wms_pick_v _pv in _jhds)
                            {
                                drjhd = dtjhd.NewRow();
                                drjhd["Mingcheng"] = _pv.ShangpinMC;
                                drjhd["Shuliang"] = _pv.DaijianSL;
                                drjhd["ShixiaoRQ"] = _pv.ShixiaoRQ;
                                drjhd["Guige"] = _pv.Guige;
                                drjhd["Pihao"] = _pv.Pihao;
                                drjhd["Kuwei"] = _pv.Kuwei;
                                drjhd["ShijianSL"] = _pv.ShijianSL;
                                drjhd["Fuhe"] = _pv.Fuhe;
                                drjhd["Zhucezheng"] = _pv.Zhucezheng;
                                dtjhd.Rows.Add(drjhd);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["JianHuoDan"]));
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }
}