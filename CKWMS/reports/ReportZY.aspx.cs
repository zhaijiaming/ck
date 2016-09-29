using System;
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
        private Iwms_chukumxService ob_wms_chukumxservice = ServiceFactory.wms_chukumxservice;
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
                    _outid = Request.QueryString["out"] ?? "";
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
                                drjhd["ShixiaoRQ"] = string.Format("{0:yyyy-MM-dd}", _pv.ShixiaoRQ);
                                drjhd["Guige"] = _pv.Guige;
                                drjhd["Pihao"] = _pv.Pihao;
                                drjhd["Kuwei"] = _pv.Kuwei;
                                drjhd["ShijianSL"] = _pv.ShijianSL;
                                drjhd["Fuhe"] = _pv.Fuhe;
                                drjhd["Zhucezheng"] = _pv.Zhucezheng;
                                drjhd["ShengchanRQ"] = string.Format("{0:yyyy-MM-dd}", _pv.ShengchanRQ);
                                drjhd["Changjia"] = _pv.Changjia;
                                dtjhd.Rows.Add(drjhd);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["JianHuoDan"]));

                            wms_chukudan tempdata = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid));
                            DataTable dtjhdt = _rds.Tables["ChuKuDan"];
                            DataRow drjhdt =dtjhdt.NewRow();
                            drjhdt["Yunsongdizhi"] = tempdata.Yunsongdizhi;
                            drjhdt["Beizhu"] = tempdata.Beizhu;
                            drjhdt["ChukuRQ"] = string.Format("{0:yyyy-MM-dd}", tempdata.ChukuRQ);
                            drjhdt["ChukudanBH"] = tempdata.ChukudanBH;
                            drjhdt["Lianxiren"] = tempdata.Lianxiren;
                            drjhdt["LianxiDH"] = tempdata.LianxiDH;
                            base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == tempdata.HuozhuID);
                            drjhdt["HuozhuID"] = wtkhdata.Kehumingcheng;
                            drjhdt["KehuMC"] = tempdata.KehuMC;
                            dtjhdt.Rows.Add(drjhdt);
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["ChuKuDan"]));
                            break;
                        case "tongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dttx = _rds.Tables["TongXingDan"];
                            var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, false, p => p.Guige);
                            DataRow drtx;
                            foreach (wms_chukumx _mx in _ckmxs)
                            {
                                drtx = dttx.NewRow();
                                drtx["Guige"] = _mx.Guige;
                                drtx["ShangpinMC"] = _mx.ShangpinMC;
                                drtx["Pihao"] = _mx.Pihao;
                                drtx["ShixiaoRQ"] = string.Format("{0:yyyy-MM-dd}",_mx.ShixiaoRQ);
                                drtx["Zhucezheng"] = _mx.Zhucezheng;
                                drtx["ChukuSL"] = _mx.ChukuSL;
                                drtx["JibenDW"] = _mx.JibenDW;
                                drtx["Changjia"] = _mx.Changjia;
                                base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia);
                                drtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH;
                                drtx["BeianBH"] = _scqy.BeianBH;
                                wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid));
                                drtx["ChunyunYQ"] = _ckd.ChunyunYQ;
                                dttx.Rows.Add(drtx);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid));
                            DataTable dtckd = _rds.Tables["ChuKuDan"];
                            DataRow drckd = dtckd.NewRow();
                            drckd["ChukudanBH"] = ckd.ChukudanBH;
                            drckd["KehuMC"] = ckd.KehuMC;
                            drckd["Yunsongdizhi"] = ckd.Yunsongdizhi;
                            drckd["ChukuRQ"] = string.Format("{0:yyyy-MM-dd}", ckd.ChukuRQ);
                            drckd["Lianxiren"] = ckd.Lianxiren;
                            drckd["LianxiDH"] = ckd.LianxiDH;

                            base_weituokehu wtkhdata1 = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID);
                            drckd["HuozhuID"] = wtkhdata1.Kehumingcheng;

                            dtckd.Rows.Add(drckd);
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["ChuKuDan"]));
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }
}