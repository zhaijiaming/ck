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
using CKWMS.App_Code;

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
                string _rkysid = "";
                ReportDataSetZY _rds = new ReportDataSetZY();
                //DataTable _dt;                
                if (Request.QueryString["pid"] != null)
                {
                    name = Request.QueryString["pid"];
                    _outid = Request.QueryString["out"] ?? "";
                    _rkysid = Request.QueryString["rkysid"] ?? "";
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
                        case "CKFuheDan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptCKFuheDan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtfh = _rds.Tables["CKFuheDan"];
                            var _fhs = ServiceFactory.quan_chukufhservice.GetOutcheckByCK(int.Parse(_outid), p => p.JianhuoSL > 0);
                            DataRow drfh;
                            foreach (quan_outcheck_v _fh in _fhs)
                            {
                                drfh = dtfh.NewRow();
                                drfh["ShangpinMC"] = _fh.ShangpinMC;
                                drfh["Zhucezheng"] = _fh.Zhucezheng;
                                drfh["Guige"] = _fh.Guige;
                                drfh["Pihao"] = _fh.Pihao;
                                drfh["Pihao1"] = _fh.Pihao1;
                                drfh["Xuliema"] = _fh.Xuliema;
                                drfh["ShengchanRQ"] = string.Format("{0:yyyy-MM-dd}", _fh.ShengchanRQ) ;
                                drfh["ShixiaoRQ"] = string.Format("{0:yyyy-MM-dd}", _fh.ShixiaoRQ);
                                drfh["JianhuoSL"] = _fh.JianhuoSL;
                                drfh["Changjia"] = _fh.Changjia;
                                drfh["Chandi"] = _fh.Chandi;
                                drfh["FuheSL"] = _fh.FuheSL;
                                if (_fh.Fuhe == null)
                                {
                                    drfh["Fuhe"] = MvcApplication.CheckResult[0];
                                }
                                else
                                {
                                    drfh["Fuhe"] = MvcApplication.CheckResult[(int)_fh.Fuhe];
                                }
                                drfh["Fuheren"] = _fh.Fuheren;
                                drfh["FuheSM"] = _fh.FuheSM;
                                drfh["MakeDate"] = string.Format("{0:yyyy-MM-dd}", _fh.MakeDate); 
                                dtfh.Rows.Add(drfh);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["CKFuheDan"]));

                            wms_chukudan _ckfhds = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid));
                            DataTable dtckfh = _rds.Tables["ChuKuDan"];
                            DataRow drckfh = dtckfh.NewRow();
                            drckfh["Yunsongdizhi"] = _ckfhds.Yunsongdizhi;
                            drckfh["Beizhu"] = _ckfhds.Beizhu;
                            drckfh["ChukuRQ"] = string.Format("{0:yyyy-MM-dd}", _ckfhds.ChukuRQ);
                            drckfh["ChukudanBH"] = _ckfhds.ChukudanBH;
                            drckfh["Lianxiren"] = _ckfhds.Lianxiren;
                            drckfh["LianxiDH"] = _ckfhds.LianxiDH;
                            base_weituokehu fh_wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == _ckfhds.HuozhuID);
                            drckfh["HuozhuID"] = fh_wtkhdata.Kehumingcheng;
                            drckfh["KehuMC"] = _ckfhds.KehuMC;
                            dtckfh.Rows.Add(drckfh);
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["ChuKuDan"]));
                            break;
                        case "CKFuhejianyan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptCKFuheJYdan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtfhjy = _rds.Tables["CKFuheDan"];
                            var _fhjys = ServiceFactory.quan_chukufhservice.GetOutcheckByCK(int.Parse(_outid), p => p.JianhuoSL > 0);
                            DataRow drfhjy;
                            foreach (quan_outcheck_v _fh in _fhjys)
                            {
                                drfhjy = dtfhjy.NewRow();
                                drfhjy["ShangpinMC"] = _fh.ShangpinMC;
                                drfhjy["Zhucezheng"] = _fh.Zhucezheng;
                                drfhjy["Guige"] = _fh.Guige;
                                drfhjy["Pihao"] = _fh.Pihao;
                                drfhjy["Pihao1"] = _fh.Pihao1;
                                drfhjy["Xuliema"] = _fh.Xuliema;
                                drfhjy["ShengchanRQ"] = string.Format("{0:yyyy-MM-dd}", _fh.ShengchanRQ);
                                drfhjy["ShixiaoRQ"] = string.Format("{0:yyyy-MM-dd}", _fh.ShixiaoRQ);
                                drfhjy["JianhuoSL"] = _fh.JianhuoSL;
                                drfhjy["Changjia"] = _fh.Changjia;
                                drfhjy["Chandi"] = _fh.Chandi;
                                
                                dtfhjy.Rows.Add(drfhjy);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["CKFuheDan"]));

                            wms_chukudan _ckfhjys = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid));
                            DataTable dtckfhjy = _rds.Tables["ChuKuDan"];
                            DataRow drckfhjy = dtckfhjy.NewRow();
                            drckfhjy["Yunsongdizhi"] = _ckfhjys.Yunsongdizhi;
                            drckfhjy["Beizhu"] = _ckfhjys.Beizhu;
                            drckfhjy["ChukuRQ"] = string.Format("{0:yyyy-MM-dd}", _ckfhjys.ChukuRQ);
                            drckfhjy["ChukudanBH"] = _ckfhjys.ChukudanBH;
                            drckfhjy["Lianxiren"] = _ckfhjys.Lianxiren;
                            drckfhjy["LianxiDH"] = _ckfhjys.LianxiDH;
                            base_weituokehu fhjy_wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == _ckfhjys.HuozhuID);
                            drckfhjy["HuozhuID"] = fhjy_wtkhdata.Kehumingcheng;
                            drckfhjy["KehuMC"] = _ckfhjys.KehuMC;
                            dtckfhjy.Rows.Add(drckfhjy);
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["ChuKuDan"]));
                            break;
                        case "YanShouBaoGao":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptRKyanshouBGdan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtzlysbg = _rds.Tables["RKYanshouDan"];
                            var _zlysbgs = ServiceFactory.quan_rukuysservice.GetEntrycheckByRK(int.Parse(_rkysid)).ToList<quan_entrycheck_v>();
                            DataRow drzlysbg;
                            foreach (quan_entrycheck_v _pr in _zlysbgs)
                            {
                                drzlysbg = dtzlysbg.NewRow();
                                //供应商
                                wms_rukudan rkd = ServiceFactory.wms_rukudanservice.GetEntityById(s=>s.ID == int.Parse(_rkysid));
                                base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == rkd.GongyingshangID);
                                drzlysbg["GYSMingcheng"] = gys.Mingcheng;
                                //厂家
                                drzlysbg["Changjia"] = _pr.Changjia;
                                drzlysbg["ShangpinMC"] = _pr.ShangpinMC;
                                drzlysbg["Guige"] = _pr.Guige;
                                drzlysbg["Pihao"] = _pr.Pihao;
                                drzlysbg["ShengchanRQ"] = string.Format("{0:yyyy-MM-dd}", _pr.ShengchanRQ);
                                drzlysbg["ShixiaoRQ"] = string.Format("{0:yyyy-MM-dd}", _pr.ShixiaoRQ);
                                drzlysbg["Zhucezheng"] = _pr.Zhucezheng;
                                drzlysbg["Shuliang"] = _pr.Shuliang;
                                //验收结果
                                if (_pr.ysresult == null)
                                {
                                    drzlysbg["ysresult"] = MvcApplication.CheckResult[0];
                                }
                                else
                                {
                                    drzlysbg["ysresult"] = MvcApplication.CheckResult[(int)_pr.ysresult];
                                    var ysresult = MvcApplication.CheckResult[(int)_pr.ysresult];
                                    if (ysresult == "合格")
                                    {
                                        drzlysbg["YanshouHGSL"] = _pr.YanshouSL;
                                        drzlysbg["YanshouBHGSL"] = "";
                                    }
                                    if (ysresult == "不合格")
                                    {
                                        drzlysbg["YanshouHGSL"] = "";
                                        drzlysbg["YanshouBHGSL"] = _pr.YanshouSL;
                                    }
                                }
                                drzlysbg["ystime"] = string.Format("{0:yyyy-MM-dd}", _pr.ystime);
                                
                                dtzlysbg.Rows.Add(drzlysbg);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["RKYanshouDan"]));
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }
}