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
using System.Web.Mvc;
using CKWMS.Common;
using System.Linq.Expressions;

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
                string _rkxdid = "";
                string _sjdid = "";
                string _rkmxid = "";
                string _zlgl_rkysid = "";
                string _zlgl_ckfhid = "";
                string _spxx_huozhuid = "";
                ReportDataSetZY _rds = new ReportDataSetZY();
                //DataTable _dt;                
                if (Request.QueryString["pid"] != null)
                {
                    name = Request.QueryString["pid"];
                    _outid = Request.QueryString["out"] ?? "";
                    _rkysid = Request.QueryString["rkysid"] ?? "";
                    _rkxdid = Request.QueryString["rkxdid"] ?? "";
                    _sjdid = Request.QueryString["sjdid"] ?? "";
                    _rkmxid = Request.QueryString["rkmxid"] ?? "";
                    _zlgl_rkysid = Request.QueryString["zlgl_rkysid"] ?? "";
                    _zlgl_ckfhid = Request.QueryString["zlgl_ckfhid"] ?? "";
                    _spxx_huozhuid = Request.QueryString["spxx_huozhuid"] ?? "";
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
                            long jhd_DaijianSLs = 0;
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
                                jhd_DaijianSLs += (long)_pv.DaijianSL;
                                dtjhd.Rows.Add(drjhd);
                            }
                            drjhd = dtjhd.NewRow();
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["JianHuoDan"]));

                            wms_chukudan tempdata = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid));
                            DataTable dtjhdt = _rds.Tables["ChuKuDan"];
                            DataRow drjhdt = dtjhdt.NewRow();
                            drjhdt["Yunsongdizhi"] = tempdata.Yunsongdizhi;
                            drjhdt["ChukuRQ"] = string.Format("{0:yyyy-MM-dd}", tempdata.ChukuRQ);
                            drjhdt["ChukudanBH"] = tempdata.ChukudanBH;
                            drjhdt["Lianxiren"] = tempdata.Lianxiren;
                            drjhdt["LianxiDH"] = tempdata.LianxiDH;
                            drjhdt["jhddjSLs"] = jhd_DaijianSLs;
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
                            long tx_ChukuSL = 0;
                            foreach (wms_chukumx _mx in _ckmxs)
                            {
                                drtx = dttx.NewRow();
                                drtx["Guige"] = _mx.Guige;
                                drtx["ShangpinMC"] = _mx.ShangpinMC;
                                drtx["Pihao"] = _mx.Pihao;
                                drtx["Xuliema"] = _mx.Xuliema;
                                drtx["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ);
                                drtx["Zhucezheng"] = _mx.Zhucezheng;
                                drtx["ChukuSL"] = _mx.ChukuSL;
                                drtx["JibenDW"] = _mx.JibenDW;
                                drtx["Changjia"] = _mx.Changjia;
                                base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia);
                                drtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH;
                                drtx["BeianBH"] = _scqy.BeianBH;
                                wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid));
                                drtx["ChunyunYQ"] = _ckd.ChunyunYQ;
                                tx_ChukuSL += (long)_mx.ChukuSL;
                                dttx.Rows.Add(drtx);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid));
                            DataTable dtckd = _rds.Tables["ChuKuDan"];
                            DataRow drckd = dtckd.NewRow();
                            drckd["ChukudanBH"] = ckd.ChukudanBH;
                            drckd["KehuMC"] = ckd.KehuMC;
                            drckd["Yunsongdizhi"] = ckd.Yunsongdizhi;
                            drckd["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ);
                            drckd["Lianxiren"] = ckd.Lianxiren;
                            drckd["LianxiDH"] = ckd.LianxiDH;
                            drckd["txckSLs"] = tx_ChukuSL;
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
                            long ckfhbg_JianhuoSLs = 0;
                            long ckfhbg_FuheSLs = 0;
                            foreach (quan_outcheck_v _fh in _fhs)
                            {
                                drfh = dtfh.NewRow();
                                drfh["ShangpinMC"] = _fh.ShangpinMC;
                                drfh["Zhucezheng"] = _fh.Zhucezheng;
                                drfh["Guige"] = _fh.Guige;
                                drfh["Pihao"] = _fh.Pihao;
                                drfh["Pihao1"] = _fh.Pihao1;
                                drfh["Xuliema"] = _fh.Xuliema;
                                drfh["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _fh.ShengchanRQ);
                                drfh["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _fh.ShixiaoRQ);
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
                                if (_fh.Fuheren != null)
                                {
                                    drfh["Fuheren"] = _fh.Fuheren;
                                }
                                if (_fh.FuheSM != null)
                                {
                                    drfh["FuheSM"] = _fh.FuheSM;
                                }
                                drfh["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _fh.MakeDate);
                                ckfhbg_JianhuoSLs += (long)_fh.JianhuoSL;
                                if (_fh.FuheSL != null)
                                {
                                    ckfhbg_FuheSLs += (long)_fh.FuheSL;
                                }
                                dtfh.Rows.Add(drfh);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["CKFuheDan"]));

                            wms_chukudan _ckfhds = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid));
                            DataTable dtckfh = _rds.Tables["ChuKuDan"];
                            DataRow drckfh = dtckfh.NewRow();
                            drckfh["Yunsongdizhi"] = _ckfhds.Yunsongdizhi;
                            drckfh["Beizhu"] = _ckfhds.Beizhu;
                            drckfh["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", _ckfhds.ChukuRQ);
                            drckfh["ChukudanBH"] = _ckfhds.ChukudanBH;
                            drckfh["Lianxiren"] = _ckfhds.Lianxiren;
                            drckfh["LianxiDH"] = _ckfhds.LianxiDH;
                            base_weituokehu fh_wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == _ckfhds.HuozhuID);
                            drckfh["HuozhuID"] = fh_wtkhdata.Kehumingcheng;
                            drckfh["KehuMC"] = _ckfhds.KehuMC;
                            drckfh["ckfhbgjhSLs"] = ckfhbg_JianhuoSLs;
                            drckfh["ckfhbgfhSLs"] = ckfhbg_FuheSLs;
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
                            long ckfujy_JianhuoSLs = 0;
                            foreach (quan_outcheck_v _fh in _fhjys)
                            {
                                drfhjy = dtfhjy.NewRow();
                                drfhjy["ShangpinMC"] = _fh.ShangpinMC;
                                drfhjy["Zhucezheng"] = _fh.Zhucezheng;
                                drfhjy["Guige"] = _fh.Guige;
                                drfhjy["Pihao"] = _fh.Pihao;
                                drfhjy["Pihao1"] = _fh.Pihao1;
                                drfhjy["Xuliema"] = _fh.Xuliema;
                                drfhjy["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _fh.ShengchanRQ);
                                drfhjy["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _fh.ShixiaoRQ);
                                drfhjy["JianhuoSL"] = _fh.JianhuoSL;
                                drfhjy["Changjia"] = _fh.Changjia;
                                drfhjy["Chandi"] = _fh.Chandi;
                                ckfujy_JianhuoSLs += (long)_fh.JianhuoSL;
                                dtfhjy.Rows.Add(drfhjy);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["CKFuheDan"]));

                            wms_chukudan _ckfhjys = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid));
                            DataTable dtckfhjy = _rds.Tables["ChuKuDan"];
                            DataRow drckfhjy = dtckfhjy.NewRow();
                            drckfhjy["Yunsongdizhi"] = _ckfhjys.Yunsongdizhi;
                            drckfhjy["Beizhu"] = _ckfhjys.Beizhu;
                            drckfhjy["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", _ckfhjys.ChukuRQ);
                            drckfhjy["ChukudanBH"] = _ckfhjys.ChukudanBH;
                            drckfhjy["Lianxiren"] = _ckfhjys.Lianxiren;
                            drckfhjy["LianxiDH"] = _ckfhjys.LianxiDH;
                            base_weituokehu fhjy_wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == _ckfhjys.HuozhuID);
                            drckfhjy["HuozhuID"] = fhjy_wtkhdata.Kehumingcheng;
                            drckfhjy["KehuMC"] = _ckfhjys.KehuMC;
                            drckfhjy["ckfhjyjhSLs"] = ckfujy_JianhuoSLs;
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
                            long rkysbg_Shuliangs = 0;
                            long rkysbg_YanshouSL_Y = 0;
                            long rkysbg_YanshouSL_N = 0;
                            foreach (quan_entrycheck_v _pr in _zlysbgs)
                            {
                                drzlysbg = dtzlysbg.NewRow();
                                //供应商
                                wms_rukudan rkd = ServiceFactory.wms_rukudanservice.GetEntityById(s => s.ID == int.Parse(_rkysid));
                                base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == rkd.GongyingshangID);
                                drzlysbg["GYSMingcheng"] = gys.Mingcheng;
                                drzlysbg["ystime"] = string.Format("{0:yyyy/MM/dd}", rkd.RukuRQ);
                                drzlysbg["Changjia"] = _pr.Changjia;
                                drzlysbg["ShangpinMC"] = _pr.ShangpinMC;
                                drzlysbg["Guige"] = _pr.Guige;
                                drzlysbg["Pihao"] = _pr.Pihao;
                                drzlysbg["Xuliema"] = _pr.Xuliema;
                                drzlysbg["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ);
                                drzlysbg["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ);
                                drzlysbg["Zhucezheng"] = _pr.Zhucezheng;
                                drzlysbg["Shuliang"] = _pr.Shuliang;
                                rkysbg_Shuliangs += (long)_pr.Shuliang;
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
                                        rkysbg_YanshouSL_Y += (long)_pr.YanshouSL;
                                    }
                                    if (ysresult == "不合格")
                                    {
                                        drzlysbg["YanshouHGSL"] = "";
                                        drzlysbg["YanshouBHGSL"] = _pr.YanshouSL;
                                        rkysbg_YanshouSL_N += (long)_pr.YanshouSL;
                                    }
                                }
                                dtzlysbg.Rows.Add(drzlysbg);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["RKYanshouDan"]));

                            DataTable dtrkfhjy = _rds.Tables["RKYanshouDanTitle"];
                            DataRow drrkfhjy = dtrkfhjy.NewRow();
                            wms_rukudan rkd_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkysid));
                            base_weituokehu wtkh_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == rkd_others.HuozhuID);
                            var _ysbgs = ServiceFactory.quan_rukuysservice.GetEntrycheckByRK(int.Parse(_rkysid)).ToList<quan_entrycheck_v>();
                            drrkfhjy["HuozhuID"] = wtkh_others.Kehumingcheng;
                            drrkfhjy["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", rkd_others.RukuRQ);
                            drrkfhjy["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _ysbgs[0].ystime);
                            userinfo man = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == (int)Session["user_id"]);
                            drrkfhjy["MakeMan"] = man.FullName;
                            drrkfhjy["rkysbgSLs"] = rkysbg_Shuliangs;
                            drrkfhjy["rkysbgYs"] = rkysbg_YanshouSL_Y;
                            drrkfhjy["rkysbgNs"] = rkysbg_YanshouSL_N;

                            dtrkfhjy.Rows.Add(drrkfhjy);
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["RKYanshouDanTitle"]));
                            break;
                        case "YanShouJiLu":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptRKYanShouJiLu.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtrkysjl = _rds.Tables["RKYanshouDan"];
                            var _rkysjls = ServiceFactory.quan_rukuysservice.GetEntrycheckByRK(int.Parse(_rkysid)).ToList<quan_entrycheck_v>();
                            DataRow drrkysjl;
                            long ysjl_Shuliangs = 0;
                            foreach (quan_entrycheck_v _pr in _rkysjls)
                            {
                                drrkysjl = dtrkysjl.NewRow();
                                //供应商
                                wms_rukudan rkd = ServiceFactory.wms_rukudanservice.GetEntityById(s => s.ID == int.Parse(_rkysid));
                                base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == rkd.GongyingshangID);
                                drrkysjl["GYSMingcheng"] = gys.Mingcheng;
                                drrkysjl["ystime"] = string.Format("{0:yyyy/MM/dd}", rkd.RukuRQ);
                                drrkysjl["Changjia"] = _pr.Changjia;
                                drrkysjl["ShangpinMC"] = _pr.ShangpinMC;
                                drrkysjl["Guige"] = _pr.Guige;
                                drrkysjl["Pihao"] = _pr.Pihao;
                                drrkysjl["Xuliema"] = _pr.Xuliema;
                                drrkysjl["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ);
                                drrkysjl["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ);
                                drrkysjl["Zhucezheng"] = _pr.Zhucezheng;
                                drrkysjl["Shuliang"] = _pr.Shuliang;
                                drrkysjl["ChunyunYQ"] = rkd.ChunyunYQ;
                                ysjl_Shuliangs += (long)_pr.Shuliang;

                                dtrkysjl.Rows.Add(drrkysjl);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["RKYanshouDan"]));

                            DataTable dtysjl = _rds.Tables["RKYanshouDanTitle"];
                            DataRow drysjl = dtysjl.NewRow();
                            wms_rukudan rkjl_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkysid));
                            base_weituokehu wtkhjl_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == rkjl_others.HuozhuID);
                            drysjl["HuozhuID"] = wtkhjl_others.Kehumingcheng;
                            drysjl["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", rkjl_others.RukuRQ);
                            drysjl["MakeDate"] = string.Format("{0:yyyy/MM/dd}", rkjl_others.MakeDate);
                            drysjl["rkysjlSLs"] = ysjl_Shuliangs;

                            dtysjl.Rows.Add(drysjl);
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["RKYanshouDanTitle"]));
                            break;
                        case "RuKuXiangDan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptShouHuoList.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtshlist = _rds.Tables["ShouHuoList"];
                            var _shlist = ServiceFactory.wms_cunhuoservice.GetUploadList(int.Parse(_rkxdid));
                            DataRow drshlist;
                            long rkxd_Shuliangs = 0;
                            foreach (wms_upload_v _sh in _shlist)
                            {
                                drshlist = dtshlist.NewRow();
                                drshlist["ShangpinMC"] = _sh.ShangpinMC;
                                drshlist["Zhucezheng"] = _sh.Zhucezheng;
                                drshlist["Guige"] = _sh.Guige;
                                drshlist["Pihao"] = _sh.Pihao;
                                drshlist["Pihao1"] = _sh.Pihao1;
                                drshlist["Xuliema"] = _sh.Xuliema;
                                drshlist["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _sh.ShengchanRQ);
                                drshlist["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _sh.ShixiaoRQ);
                                drshlist["Changjia"] = _sh.Changjia;
                                drshlist["Chandi"] = _sh.Chandi;
                                drshlist["Shuliang"] = _sh.Shuliang;
                                drshlist["Kuwei"] = _sh.Kuwei;
                                drshlist["CunhuoSM"] = _sh.CunhuoSM;
                                drshlist["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _sh.MakeDate);
                                rkxd_Shuliangs += (long)_sh.Shuliang;

                                dtshlist.Rows.Add(drshlist);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["ShouHuoList"]));

                            DataTable dtshlist_others = _rds.Tables["ShouHuoListTitle"];
                            DataRow drshlist_others = dtshlist_others.NewRow();
                            wms_rukudan rkshlist_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkxdid));
                            base_weituokehu wtkhshlist_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == rkshlist_others.HuozhuID);
                            drshlist_others["RukudanBH"] = rkshlist_others.RukudanBH;
                            drshlist_others["HuozhuID"] = wtkhshlist_others.Kehumingcheng;
                            drshlist_others["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", rkshlist_others.RukuRQ);
                            drshlist_others["ChunyunYQ"] = rkshlist_others.ChunyunYQ;
                            drshlist_others["rkxdSLs"] = rkxd_Shuliangs;

                            dtshlist_others.Rows.Add(drshlist_others);
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["ShouHuoListTitle"]));
                            break;
                        case "ShangJiaList":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptShangJiaList.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtsjlist = _rds.Tables["ShouHuoList"];
                            var _sjlist = ServiceFactory.wms_cunhuoservice.GetUploadList(int.Parse(_sjdid));
                            DataRow drsjlist;
                            long sjl_sshuliangs = 0;
                            foreach (wms_upload_v _sh in _sjlist)
                            {
                                drsjlist = dtsjlist.NewRow();
                                drsjlist["ShangpinMC"] = _sh.ShangpinMC;
                                drsjlist["Zhucezheng"] = _sh.Zhucezheng;
                                drsjlist["Guige"] = _sh.Guige;
                                drsjlist["Pihao"] = _sh.Pihao;
                                drsjlist["Pihao1"] = _sh.Pihao1;
                                drsjlist["Xuliema"] = _sh.Xuliema;
                                drsjlist["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _sh.ShengchanRQ);
                                drsjlist["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _sh.ShixiaoRQ);
                                drsjlist["Changjia"] = _sh.Changjia;
                                drsjlist["Chandi"] = _sh.Chandi;
                                drsjlist["Shuliang"] = _sh.Shuliang;
                                drsjlist["Kuwei"] = _sh.Kuwei;
                                drsjlist["CunhuoSM"] = _sh.CunhuoSM;
                                drsjlist["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _sh.MakeDate);
                                sjl_sshuliangs += (long)_sh.Shuliang;

                                dtsjlist.Rows.Add(drsjlist);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["ShouHuoList"]));

                            DataTable dtsjlist_others = _rds.Tables["ShouHuoListTitle"];
                            DataRow drsjlist_others = dtsjlist_others.NewRow();
                            wms_rukudan rksjlist_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_sjdid));
                            base_weituokehu wtkhsjlist_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == rksjlist_others.HuozhuID);
                            drsjlist_others["RukudanBH"] = rksjlist_others.RukudanBH;
                            drsjlist_others["HuozhuID"] = wtkhsjlist_others.Kehumingcheng;
                            drsjlist_others["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", rksjlist_others.RukuRQ);
                            drsjlist_others["ChunyunYQ"] = rksjlist_others.ChunyunYQ;
                            drsjlist_others["sjlSLs"] = sjl_sshuliangs;

                            dtsjlist_others.Rows.Add(drsjlist_others);
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["ShouHuoListTitle"]));
                            break;
                        case "RuKuMX":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptRuKumingxi.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtRuKuMX = _rds.Tables["RuKumingxi"];
                            var _RuKuMXs = ServiceFactory.wms_rukumxservice.LoadSortEntities(p => p.RukuID == int.Parse(_rkmxid) && p.IsDelete == false, true, s => s.ShangpinMC);
                            DataRow drRuKuMX;
                            long rkmx_DaohuoSLs = 0;
                            foreach (wms_rukumx _pr in _RuKuMXs)
                            {
                                drRuKuMX = dtRuKuMX.NewRow();
                                //drRuKuMX["ShangpinMC"] = _pr.ShangpinMC;
                                drRuKuMX["Guige"] = _pr.Guige;
                                drRuKuMX["Pihao"] = _pr.Pihao;
                                drRuKuMX["Zhucezheng"] = _pr.Zhucezheng;
                                //drRuKuMX["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ);
                                drRuKuMX["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ);
                                drRuKuMX["DaohuoSL"] = _pr.DaohuoSL;
                                //drRuKuMX["Chandi"] = _pr.Chandi;
                                rkmx_DaohuoSLs += (long)_pr.DaohuoSL;

                                dtRuKuMX.Rows.Add(drRuKuMX);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["RuKumingxi"]));

                            DataTable dtRuKuMX_others = _rds.Tables["RuKumingxi_Title"];
                            DataRow drRuKuMX_others = dtRuKuMX_others.NewRow();
                            wms_rukudan RuKuMX_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkmxid));
                            base_weituokehu wtkhRuKuMX_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == RuKuMX_others.HuozhuID);
                            drRuKuMX_others["RukudanBH"] = RuKuMX_others.RukudanBH;
                            drRuKuMX_others["HuozhuID"] = wtkhRuKuMX_others.Kehumingcheng;
                            drRuKuMX_others["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", RuKuMX_others.RukuRQ);
                            drRuKuMX_others["ChunyunYQ"] = RuKuMX_others.ChunyunYQ;
                            base_gongyingshang RuKuMXgys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == RuKuMX_others.GongyingshangID);
                            drRuKuMX_others["GongyingshangID"] = RuKuMXgys.Mingcheng;
                            wms_cangku RuKuMX_ck = ServiceFactory.wms_cangkuservice.GetEntityById(p => p.ID == RuKuMX_others.CangkuID);
                            drRuKuMX_others["CangkuID"] = RuKuMX_ck.Mingcheng;
                            drRuKuMX_others["rkmxdhSLs"] = rkmx_DaohuoSLs;

                            dtRuKuMX_others.Rows.Add(drRuKuMX_others);
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["RuKumingxi_Title"]));
                            break;
                        case "zlgl_RukuYanshouBaogao":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptZlgl_RKBG.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtzlgl_bg = _rds.Tables["zlgl_RKYSBG"];
                            var _zlgl_rkyss = ServiceFactory.quan_rukuysservice.GetInrec(p => p.RukudanBH == _zlgl_rkysid).ToList<quan_inrec_v>();
                            DataRow drzlgl_bg;
                            long zlgl_bg_Shuliang = 0;
                            long zlgl_bg_YanshouHGSL = 0;
                            long zlgl_bg_YanshouBHGSL = 0;
                            foreach (quan_inrec_v _pr in _zlgl_rkyss)
                            {
                                drzlgl_bg = dtzlgl_bg.NewRow();
                                base_gongyingshang zlgl_gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == (int)_pr.GongyingshangID);
                                drzlgl_bg["GongyingshangID"] = zlgl_gys.Mingcheng;
                                drzlgl_bg["Changjia"] = _pr.Changjia;
                                drzlgl_bg["ShangpinMC"] = _pr.ShangpinMC;
                                drzlgl_bg["Guige"] = _pr.Guige;
                                drzlgl_bg["Pihao"] = _pr.Pihao;
                                drzlgl_bg["Xuliema"] = _pr.Xuliema;
                                drzlgl_bg["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ);
                                drzlgl_bg["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ);
                                drzlgl_bg["Zhucezheng"] = _pr.Zhucezheng;
                                drzlgl_bg["Shuliang"] = _pr.Shuliang;
                                //验收结果
                                drzlgl_bg["ysresult"] = MvcApplication.CheckResult[_pr.ysresult];
                                var ysresult = MvcApplication.CheckResult[_pr.ysresult];
                                if (ysresult == "合格")
                                {
                                    drzlgl_bg["YanshouHGSL"] = _pr.YanshouSL;
                                    drzlgl_bg["YanshouBHGSL"] = "";
                                    zlgl_bg_YanshouHGSL += (long)_pr.YanshouSL;
                                }
                                if (ysresult == "不合格")
                                {
                                    drzlgl_bg["YanshouHGSL"] = "";
                                    drzlgl_bg["YanshouBHGSL"] = _pr.YanshouSL;
                                    zlgl_bg_YanshouBHGSL += (long)_pr.YanshouSL;
                                }
                                drzlgl_bg["ystime"] = string.Format("{0:yyyy/MM/dd}", _pr.ystime);
                                zlgl_bg_Shuliang += (long)_pr.Shuliang;

                                dtzlgl_bg.Rows.Add(drzlgl_bg);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["zlgl_RKYSBG"]));

                            DataTable dtzlgl_bg_others = _rds.Tables["zlgl_RKYSBG_Title"];
                            DataRow drzlgl_bg_others = dtzlgl_bg_others.NewRow();
                            //得到第一个货主ID
                            quan_inrec_v zlgl_huozhiid_first = _zlgl_rkyss.FirstOrDefault();
                            base_weituokehu zlgl_wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == zlgl_huozhiid_first.HuozhuID);
                            drzlgl_bg_others["HuozhuID"] = zlgl_wtkh.Kehumingcheng;
                            userinfo zlgl_man = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == (int)Session["user_id"]);
                            drzlgl_bg_others["MakeMan"] = zlgl_man.FullName;
                            wms_rukudan zlgl_rkd = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.RukudanBH == _zlgl_rkysid);
                            drzlgl_bg_others["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", zlgl_rkd.RukuRQ);
                            drzlgl_bg_others["rkysbgSLs"] = zlgl_bg_Shuliang;
                            drzlgl_bg_others["rkysbgYs"] = zlgl_bg_YanshouHGSL;
                            drzlgl_bg_others["rkysbgNs"] = zlgl_bg_YanshouBHGSL;

                            dtzlgl_bg_others.Rows.Add(drzlgl_bg_others);
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["zlgl_RKYSBG_Title"]));
                            break;
                        case "zlgl_CukuFuheBaogao":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptZlgl_CKBG.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtzlgl_ckfhbg = _rds.Tables["zlgl_CKFHBG"];
                            var _zlgl_ckfhs = ServiceFactory.quan_chukufhservice.GetOutrec(p => p.ChukudanBH == _zlgl_ckfhid).ToList<quan_outrec_v>();
                            DataRow drzlgl_ckfhbg;
                            long zlgl_ckfhbg_Shuliang = 0;
                            long zlgl_ckfhbg_FuheSL = 0;
                            long zlgl_ckfhbg_YanshouHGSL = 0;
                            long zlgl_ckfhbg_YanshouBHGSL = 0;
                            foreach (quan_outrec_v _pr in _zlgl_ckfhs)
                            {
                                drzlgl_ckfhbg = dtzlgl_ckfhbg.NewRow();
                                drzlgl_ckfhbg["ShangpinMC"] = _pr.ShangpinMC;
                                drzlgl_ckfhbg["Zhucezheng"] = _pr.Zhucezheng;
                                drzlgl_ckfhbg["Guige"] = _pr.Guige;
                                drzlgl_ckfhbg["Pihao"] = _pr.Pihao;
                                drzlgl_ckfhbg["Pihao1"] = _pr.Pihao1;
                                drzlgl_ckfhbg["Xuliema"] = _pr.Xuliema;
                                drzlgl_ckfhbg["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ);
                                drzlgl_ckfhbg["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ);
                                drzlgl_ckfhbg["JianhuoSL"] = _pr.JianhuoSL;
                                drzlgl_ckfhbg["Changjia"] = _pr.Changjia;
                                drzlgl_ckfhbg["Chandi"] = _pr.Chandi;
                                //验收结果
                                drzlgl_ckfhbg["Fuhe"] = MvcApplication.CheckResult[_pr.Fuhe];
                                drzlgl_ckfhbg["FuheSL"] = _pr.FuheSL;
                                //var FuheResult = MvcApplication.CheckResult[_pr.Fuhe];
                                //if (FuheResult == "合格")
                                //{
                                //    drzlgl_ckfhbg["FuheHGSL"] = _pr.FuheSL;
                                //    drzlgl_ckfhbg["FuheBHGSL"] = "";
                                //    zlgl_ckfhbg_YanshouHGSL += (long)_pr.FuheSL;
                                //}
                                //if (FuheResult == "不合格")
                                //{
                                //    drzlgl_ckfhbg["FuheHGSL"] = "";
                                //    drzlgl_ckfhbg["FuheBHGSL"] = _pr.FuheSL;
                                //    zlgl_ckfhbg_YanshouBHGSL += (long)_pr.FuheSL;
                                //}
                                drzlgl_ckfhbg["Fuheren"] = _pr.Fuheren;
                                drzlgl_ckfhbg["FuheSM"] = _pr.FuheSM;
                                drzlgl_ckfhbg["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _pr.MakeDate);
                                zlgl_ckfhbg_Shuliang += (long)_pr.JianhuoSL;
                                zlgl_ckfhbg_FuheSL += (long)_pr.FuheSL;

                                dtzlgl_ckfhbg.Rows.Add(drzlgl_ckfhbg);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["zlgl_CKFHBG"]));

                            DataTable dtzlgl_ckfhbg_others = _rds.Tables["zlgl_CKFHBG_Title"];
                            DataRow drzlgl_ckfhbg_others = dtzlgl_ckfhbg_others.NewRow();
                            drzlgl_ckfhbg_others["ChukudanBH"] = _zlgl_ckfhid;
                            //得到第一个ID
                            quan_outrec_v zlgl_first = _zlgl_ckfhs.FirstOrDefault();
                            drzlgl_ckfhbg_others["KehuMC"] = zlgl_first.KehuMC;
                            wms_chukudan zlgl_ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ChukudanBH == _zlgl_ckfhid);
                            drzlgl_ckfhbg_others["Yunsongdizhi"] = zlgl_ckd.Yunsongdizhi;
                            drzlgl_ckfhbg_others["Beizhu"] = zlgl_ckd.Beizhu;
                            drzlgl_ckfhbg_others["Lianxiren"] = zlgl_ckd.Lianxiren;
                            drzlgl_ckfhbg_others["LianxiDH"] = zlgl_ckd.LianxiDH;
                            base_weituokehu zlgl_ckfh_wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == zlgl_first.HuozhuID);
                            drzlgl_ckfhbg_others["HuozhuID"] = zlgl_ckfh_wtkh.Kehumingcheng;
                            drzlgl_ckfhbg_others["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", zlgl_first.ChukuRQ);
                            //统计
                            drzlgl_ckfhbg_others["ckfhbgSLs"] = zlgl_ckfhbg_Shuliang;
                            drzlgl_ckfhbg_others["ckfhbgYs"] = zlgl_ckfhbg_YanshouHGSL;
                            drzlgl_ckfhbg_others["ckfhbgNs"] = zlgl_ckfhbg_YanshouBHGSL;
                            drzlgl_ckfhbg_others["ckfhbgFHSLs"] = zlgl_ckfhbg_FuheSL;

                            dtzlgl_ckfhbg_others.Rows.Add(drzlgl_ckfhbg_others);
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["zlgl_CKFHBG_Title"]));
                            break;
                        case "shangpinxx_shouying":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptSpxx_shouying.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtspxx = _rds.Tables["Spxx_shouying"];
                            DataRow drspxx;
                            var _spxxs = ServiceFactory.base_shangpinxxservice.LoadSortEntities(p => p.HuozhuID == int.Parse(_spxx_huozhuid) && p.IsDelete == false && p.Shouying == 5, false, p => p.HuozhuID);
                            foreach (base_shangpinxx _pr in _spxxs)
                            {
                                drspxx = dtspxx.NewRow();
                                //base_shangpinxx
                                drspxx["ZhucezhengBH"] = _pr.ZhucezhengBH;
                                drspxx["Daima"] = _pr.Daima;
                                drspxx["Guige"] = _pr.Guige;
                                drspxx["Chandi"] = _pr.Chandi;
                                //base_shangpinzcz
                                var _spzczs = ServiceFactory.base_shangpinzczservice.GetEntityById(p => p.Bianhao == _pr.ZhucezhengBH && p.IsDelete == false);
                                drspxx["Mingcheng"] = _spzczs.Mingcheng;
                                drspxx["ZhucezhengYXQ"] = string.Format("{0:yyyy-MM-dd}", _spzczs.ZhucezhengYXQ);
                                dtspxx.Rows.Add(drspxx);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["Spxx_shouying"]));

                            DataTable dtspxx_others = _rds.Tables["Spxx_shouying_Title"];
                            DataRow drspxx_others = dtspxx_others.NewRow();
                            //base_gongyingshang
                            var spxx_wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == int.Parse(_spxx_huozhuid) && p.IsDelete == false);
                            drspxx_others["Kehumingcheng"] = spxx_wtkh.Kehumingcheng;
                            drspxx_others["YingyezhizhaoBH"] = spxx_wtkh.YingyezhizhaoBH;
                            drspxx_others["YingyezhizhaoYXQ"] = string.Format("{0:yyyy-MM-dd}", spxx_wtkh.YingyezhizhaoYXQ);
                            drspxx_others["JingyingxukeBH"] = spxx_wtkh.JingyingxukeBH;
                            drspxx_others["JingyingxukeYXQ"] = string.Format("{0:yyyy-MM-dd}", spxx_wtkh.JingyingxukeYXQ);
                            drspxx_others["Lianxiren"] = spxx_wtkh.Lianxiren;
                            drspxx_others["Lianxidianhua"] = spxx_wtkh.Lianxidianhua;
                            drspxx_others["BeianBH"] = spxx_wtkh.BeianBH;
                            drspxx_others["BeianYXQ"] = string.Format("{0:yyyy-MM-dd}", spxx_wtkh.BeianYXQ);

                            dtspxx_others.Rows.Add(drspxx_others);
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["Spxx_shouying_Title"]));
                            break;
                        case "YiweiInfo":
                            int userid = (int)Session["user_id"];
                            string pagetag = "wms_yiwei_index";
                            Expression<Func<wms_yiwei, bool>> where = PredicateExtensionses.True<wms_yiwei>();
                            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
                            if (sc != null && sc.ConditionInfo != null)
                            {
                                string[] sclist = sc.ConditionInfo.Split(';');
                                foreach (string scl in sclist)
                                {
                                    string[] scld = scl.Split(',');
                                    switch (scld[0])
                                    {
                                        case "kwbh":
                                            string kwbh = scld[1];
                                            string kwbhequal = scld[2];
                                            string kwbhand = scld[3];
                                            if (!string.IsNullOrEmpty(kwbh))
                                            {
                                                if (kwbhequal.Equals("="))
                                                {
                                                    if (kwbhand.Equals("and"))
                                                        where = where.And(p => p.KWBH == kwbh);
                                                    else
                                                        where = where.Or(p => p.KWBH == kwbh);
                                                }
                                                if (kwbhequal.Equals("like"))
                                                {
                                                    if (kwbhand.Equals("and"))
                                                        where = where.And(p => p.KWBH.Contains(kwbh));
                                                    else
                                                        where = where.Or(p => p.KWBH.Contains(kwbh));
                                                }
                                            }
                                            break;
                                        case "xkwbh":
                                            string xkwbh = scld[1];
                                            string xkwbhequal = scld[2];
                                            string xkwbhand = scld[3];
                                            if (!string.IsNullOrEmpty(xkwbh))
                                            {
                                                if (xkwbhequal.Equals("="))
                                                {
                                                    if (xkwbhand.Equals("and"))
                                                        where = where.And(p => p.XKWBH == xkwbh);
                                                    else
                                                        where = where.Or(p => p.XKWBH == xkwbh);
                                                }
                                                if (xkwbhequal.Equals("like"))
                                                {
                                                    if (xkwbhand.Equals("and"))
                                                        where = where.And(p => p.XKWBH.Contains(xkwbh));
                                                    else
                                                        where = where.Or(p => p.XKWBH.Contains(xkwbh));
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            where = where.And(wms_yiwei => wms_yiwei.IsDelete == false);

                            var rptYiweiInfos = ServiceFactory.wms_yiweiservice.LoadSortEntities(where.Compile(), false, wms_yiwei => wms_yiwei.ID);
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptYiweiInfo.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtYiweiInfo = _rds.Tables["YiweiInfo"];
                            DataRow drYiweiInfo;
                            foreach (wms_yiwei _pr in rptYiweiInfos)
                            {
                                drYiweiInfo = dtYiweiInfo.NewRow();
                                wms_shouhuomx YiweiInfo_shmx = ServiceFactory.wms_shouhuomxservice.GetEntityById(p => p.ID == _pr.SHID);
                                drYiweiInfo["SHID"] = YiweiInfo_shmx.ShangpinMC + "," + YiweiInfo_shmx.Pihao + "," + YiweiInfo_shmx.Xuliema + "," + YiweiInfo_shmx.Guige;
                                //drYiweiInfo["SHID"] = HtmlHelperExtensions.GetDataValue_ID("收货", "全部", (int)_pr.SHID);
                                drYiweiInfo["KWBH"] = _pr.KWBH;
                                drYiweiInfo["Shuliang"] = _pr.Shuliang;
                                drYiweiInfo["XKWBH"] = _pr.XKWBH;
                                drYiweiInfo["MakeDate"] = string.Format("{0:d}", _pr.MakeDate);
                                userinfo YiweiInfo_man = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == _pr.MakeMan);
                                drYiweiInfo["MakeMan"] = YiweiInfo_man.FullName;
                                dtYiweiInfo.Rows.Add(drYiweiInfo);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["YiweiInfo"]));
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }
}