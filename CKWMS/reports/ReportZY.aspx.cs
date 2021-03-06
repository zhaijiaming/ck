﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKWMS.BSL;
using CKWMS.EFModels;
//using CKWMS.BSL;
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
                string _outid = "";
                string _rkysid = "";
                string _rkxdid = "";
                string _sjdid = "";
                string _rkmxid = "";
                string _zlgl_rkysid = "";
                string _zlgl_ckfhid = "";
                string _spxx_huozhuid = "";
                string _JH_ckjyid = "";
                string _JSWZ_TXD_id = "";
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
                    _JH_ckjyid = Request.QueryString["JH_ckjyid"] ?? "";
                    _JSWZ_TXD_id = Request.QueryString["JSWZ_TXD_id"] ?? "";
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
                            DataRow drjhd;
                            long jhd_DaijianSLs = 0;
                            float JianShus = 0;
                            float JianShu = 0;

                            try
                            {
                                var _jhds = ob_wms_jianhuoservice.GetPickDetail(int.Parse(_outid), p => p.DaijianSL > 0).OrderBy(p => p.Kuwei).ThenBy(p => p.Pihao).ToList<wms_pick_v>();
                                //相同库位、批号、序列码、规格、商品名称、商品代码的数据相加。
                                var afterList = _jhds.GroupBy(a => a.Kuwei + a.Pihao + a.Xuliema + a.Guige + a.ShangpinMC + a.ShangpinDM + a.JianhuoSM).Select(g => (new
                                {
                                    id = g.Key,
                                    count = g.Count(),
                                    ShangpinMC = g.First().ShangpinMC == null ? "" : g.First().ShangpinMC,
                                    DaijianSL = g.Sum(item => item.DaijianSL),
                                    Huansuanlv = g.First().Huansuanlv,
                                    ShixiaoRQ = string.Format("{0:yyyy/MM/dd}", g.First().ShixiaoRQ == null ? "" : ((DateTime)g.First().ShixiaoRQ).ToString("yyyy/MM/dd")),
                                    Guige = g.First().Guige,
                                    Pihao = g.First().Pihao,
                                    Xuliema = g.First().Xuliema,
                                    Kuwei = g.First().Kuwei,
                                    Zhucezheng = g.First().Zhucezheng,
                                    JianhuoSM = g.First().JianhuoSM,
                                })).OrderBy(t => t.Kuwei).ThenBy(t => t.Guige);

                                foreach (var _pv in afterList)
                                {
                                    drjhd = dtjhd.NewRow();
                                    drjhd["Mingcheng"] = _pv.ShangpinMC == null ? "" : _pv.ShangpinMC;
                                    if (!string.IsNullOrEmpty(_pv.ShangpinMC))
                                    {
                                        var spxx = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.Mingcheng == _pv.ShangpinMC && p.Guige == _pv.Guige && p.IsDelete == false);
                                        if (spxx != null)
                                        {
                                            drjhd["JianhuoSM"] = spxx.Baozhuangyaoqiu == null ? "" : spxx.Baozhuangyaoqiu;
                                        }
                                    }
                                    drjhd["DaijianSL"] = _pv.DaijianSL;
                                    drjhd["Huansuanlv"] = _pv.Huansuanlv;

                                    JianShu = _pv.DaijianSL / _pv.Huansuanlv == null ? int.Parse("0") : (float)_pv.DaijianSL / _pv.Huansuanlv;
                                    drjhd["JianShu"] = JianShu;
                                    JianShus += JianShu;

                                    drjhd["ShixiaoRQ"] = _pv.ShixiaoRQ;
                                    drjhd["Guige"] = _pv.Guige;
                                    drjhd["Pihao"] = _pv.Pihao;
                                    drjhd["Xuliema"] = _pv.Xuliema;
                                    drjhd["Kuwei"] = _pv.Kuwei;
                                    drjhd["Zhucezheng"] = _pv.Zhucezheng;
                                    drjhd["JianhuoSM"] = _pv.JianhuoSM;
                                    jhd_DaijianSLs += (long)_pv.DaijianSL;

                                    dtjhd.Rows.Add(drjhd);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["JianHuoDan"]));

                            DataTable dtjhdt = _rds.Tables["JianHuoDan_Title"];
                            DataRow drjhdt = dtjhdt.NewRow();
                            try
                            {
                                wms_chukudan tempdata = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (tempdata != null)
                                {
                                    drjhdt["Yunsongdizhi"] = tempdata.Yunsongdizhi;
                                    drjhdt["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", tempdata.ChukuRQ == null ? "" : ((DateTime)tempdata.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drjhdt["ChukudanBH"] = tempdata.ChukudanBH;
                                    drjhdt["Lianxiren"] = tempdata.Lianxiren;
                                    drjhdt["LianxiDH"] = tempdata.LianxiDH;
                                    drjhdt["Beizhu"] = tempdata.Beizhu;
                                    drjhdt["jhddjSLs"] = jhd_DaijianSLs;
                                    drjhdt["JianShus"] = JianShus;
                                    drjhdt["KehuMC"] = tempdata.KehuMC;
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == tempdata.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drjhdt["HuozhuID"] = wtkhdata.Kehumingcheng;
                                    }
                                    drjhdt["YunsongFS"] = MvcApplication.DeliveryType[tempdata.YunsongFS == null ? int.Parse("0") : (int)tempdata.YunsongFS];
                                    if (tempdata.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == tempdata.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drjhdt["Kuaidi"] = "";
                                        else
                                            drjhdt["Kuaidi"] = tran_company.Mingcheng;
                                    }
                                    drjhdt["JiesuanFS"] = MvcApplication.SettlingType[tempdata.JiesuanFS == null ? int.Parse("0") : (int)tempdata.JiesuanFS];
                                    dtjhdt.Rows.Add(drjhdt);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["JianHuoDan_Title"]));
                            break;
                        case "tongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dttx = _rds.Tables["TongXingDan"];
                            DataRow drtx;
                            long tx_ChukuSL = 0;
                            float ChukuJS = 0;
                            float tx_ChukuJSs = 0;
                            try
                            {
                                var _cyyq = "";
                                wms_chukudan _ckd000 = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (_ckd000 != null)
                                {
                                    _cyyq= _ckd000.ChunyunYQ == null ? "" : _ckd000.ChunyunYQ.Trim();
                                }
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, true, p => p.Pihao).ToList();
                                var _cjlist = _ckmxs.GroupBy(p =>p.Changjia ).Select(g => g.First());
                                List<base_shengchanqiye> _scqylist = new List<base_shengchanqiye>();
                                foreach(var cj in _cjlist)
                                {
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == cj.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        _scqylist.Add(_scqy);
                                        //drtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH == null ? (_scqy.BeianBH == null ? "" : _scqy.BeianBH) : _scqy.ShengchanxukeBH.Trim();
                                        //drtx["BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                    }
                                }
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drtx = dttx.NewRow();
                                    drtx["Guige"] = _mx.Guige == null ? "" : _mx.Guige.Trim();
                                    //drtx["Changjia"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();
                                    drtx["Beizhu"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();
                                    drtx["ShangpinMC"] = _mx.ShangpinMC == null ? "" : _mx.ShangpinMC.Trim();
                                    drtx["Pihao"] = _mx.Pihao == null ? "" : _mx.Pihao.Trim();
                                    drtx["Xuliema"] = _mx.Xuliema == null ? "" : _mx.Xuliema.Trim();
                                    drtx["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drtx["Zhucezheng"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();

                                    ChukuJS = _mx.JianhuoSL / _mx.Huansuanlv == null ? int.Parse("0") : (float)_mx.JianhuoSL / _mx.Huansuanlv;
                                    drtx["ChukuJS"] = ChukuJS;
                                    tx_ChukuJSs += ChukuJS;

                                    //drtx["JibenDW"] = _mx.JibenDW == null ? "" : _mx.JibenDW.Trim();
                                    drtx["JibenDW"] = _mx.BaozhuangDW == null ? "" : _mx.BaozhuangDW.Trim();
                                    drtx["BeianBH"] = _mx.ShangpinDM == null ? "" : _mx.ShangpinDM.Trim();
                                    //wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                    //if (_ckd != null)
                                    //{
                                    //    drtx["ChunyunYQ"] = _ckd.ChunyunYQ == null ? "" : _ckd.ChunyunYQ.Trim();
                                    //}
                                    drtx["ChunyunYQ"] =_cyyq;
                                    //drtx["ShengchanxukeBH"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();
                                    //base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    //if (_scqy != null)
                                    //{
                                    //    drtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH == null ? (_scqy.BeianBH == null ? "" : _scqy.BeianBH) : _scqy.ShengchanxukeBH.Trim();
                                    //    //drtx["BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                    //}
                                    foreach(var sqcy in _scqylist)
                                    {
                                        if(_mx.Changjia==sqcy.Qiyemingcheng)
                                            drtx["ShengchanxukeBH"] = sqcy.ShengchanxukeBH == null ? (sqcy.BeianBH == null ? "" : sqcy.BeianBH) : sqcy.ShengchanxukeBH.Trim();
                                    }
                                    tx_ChukuSL += (long)_mx.ChukuSL;

                                    dttx.Rows.Add(drtx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            //页眉&页脚
                            DataTable dtckd = _rds.Tables["TongXingDan_Title"];
                            DataRow drckd = dtckd.NewRow();
                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drckd["ChukudanBH"] = ckd.ChukudanBH == null ? "" : ckd.ChukudanBH.Trim();
                                    drckd["KehuMC"] = ckd.KehuMC == null ? "" : ckd.KehuMC.Trim();
                                    drckd["Yunsongdizhi"] = ckd.Yunsongdizhi == null ? "" : ckd.Yunsongdizhi.Trim();
                                    drckd["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drckd["Lianxiren"] = ckd.Lianxiren == null ? "" : ckd.Lianxiren.Trim();
                                    drckd["LianxiDH"] = ckd.LianxiDH == null ? "" : ckd.LianxiDH.Trim();
                                    drckd["Beizhu"] = ckd.Beizhu == null ? "" : ckd.Beizhu.Trim();

                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drckd["HuozhuID"] = wtkhdata.Kehumingcheng == null ? "" : wtkhdata.Kehumingcheng.Trim();
                                    }

                                    drckd["YunsongFS"] = MvcApplication.DeliveryType[ckd.YunsongFS == null ? int.Parse("0") : (int)ckd.YunsongFS];

                                    if (ckd.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == ckd.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drckd["Kuaidi"] = "";
                                        else
                                            drckd["Kuaidi"] = tran_company.Mingcheng == null ? "" : tran_company.Mingcheng.Trim();
                                    }

                                    drckd["JiesuanFS"] = MvcApplication.SettlingType[ckd.JiesuanFS == null ? int.Parse("0") : (int)ckd.JiesuanFS];
                                }
                                drckd["tx_ChukuJSs"] = tx_ChukuJSs;

                                dtckd.Rows.Add(drckd);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["TongXingDan_Title"]));
                            break;
                        case "JSGRtongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptJSGRTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtJSGRtx = _rds.Tables["TongXingDan"];
                            DataRow drJSGRtx;
                            float JSGRtx_JianhuoSLs = 0;//数量总计
                            float JSGRChukuJS = 0;//件数
                            float JSGRtx_ChukuJSs = 0;//件数总计
                            try
                            {
                                var _grcyyq = "";
                                wms_chukudan _ckd001 = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (_ckd001 != null)
                                {
                                    _grcyyq = _ckd001.ChunyunYQ == null ? "" : _ckd001.ChunyunYQ.Trim();
                                }
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, true, p => p.Pihao).ToList();
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drJSGRtx = dtJSGRtx.NewRow();
                                    drJSGRtx["Guige"] = _mx.Guige == null ? "" : _mx.Guige.Trim();
                                    drJSGRtx["ShangpinMC"] = _mx.ShangpinMC == null ? "" : _mx.ShangpinMC.Trim();
                                    drJSGRtx["Pihao"] = _mx.Pihao == null ? "" : _mx.Pihao.Trim();
                                    drJSGRtx["Xuliema"] = _mx.Xuliema == null ? "" : _mx.Xuliema.Trim();
                                    drJSGRtx["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drJSGRtx["Huansuanlv"] = string.Format("{0:0.00}", _mx.Huansuanlv);
                                    drJSGRtx["JianhuoSL"] = string.Format("{0:0.00}", _mx.JianhuoSL == null ? int.Parse("0.00") : _mx.JianhuoSL);
                                    drJSGRtx["JibenDW"] = _mx.JibenDW == null ? "" : _mx.JibenDW.Trim();

                                    var spxxData = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == _mx.ShangpinID && p.IsDelete == false);
                                    if (spxxData != null)
                                    {
                                        drJSGRtx["ShangpinMS"] = spxxData.ShangpinMS == null ? "" : spxxData.ShangpinMS;
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == spxxData.GongyingID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drJSGRtx["gongYingShangMC"] = gys.Mingcheng == null ? "" : gys.Mingcheng.Trim();
                                        }
                                    }

                                    JSGRChukuJS = _mx.JianhuoSL / _mx.Huansuanlv == null ? int.Parse("0.00") : (float)_mx.JianhuoSL / _mx.Huansuanlv;
                                    drJSGRtx["ChukuJS"] = string.Format("{0:0.00}", JSGRChukuJS);

                                    JSGRtx_ChukuJSs += (long)JSGRChukuJS;//件数总计
                                    JSGRtx_JianhuoSLs += (long)_mx.JianhuoSL;//数量总计

                                    //wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                    //if (_ckd != null)
                                    //{
                                    //    drJSGRtx["ChunyunYQ"] = _ckd.ChunyunYQ == null ? "" : _ckd.ChunyunYQ.Trim();
                                    //}
                                    //表头储运要求
                                    //drJSGRtx["ChunyunYQ"] = _grcyyq;
                                    //商品储运要求
                                    drJSGRtx["ChunyunYQ"] = spxxData.Cunchutiaojian;
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        if (_scqy.ShengchanxukeBH != null)
                                            drJSGRtx["ShengchanxukeBH"] = _scqy.Qiyemingcheng.Trim() + "/" + _scqy.ShengchanxukeBH.Trim();
                                        else
                                            drJSGRtx["ShengchanxukeBH"] = _scqy.Qiyemingcheng;
                                        //drJSGRtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH == null ? "" : _scqy.ShengchanxukeBH.Trim();
                                    }
                                    else
                                        drJSGRtx["ShengchanxukeBH"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();

                                    //注册证&备案编号2选1
                                    if (string.IsNullOrEmpty(_mx.Zhucezheng))
                                    {
                                        if (_scqy != null)
                                            drJSGRtx["Zhucezheng_BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                        else
                                            drJSGRtx["Zhucezheng_BeianBH"] = "";
                                    }
                                    else
                                    {
                                        drJSGRtx["Zhucezheng_BeianBH"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();
                                    }

                                    dtJSGRtx.Rows.Add(drJSGRtx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            //页眉&页脚
                            DataTable dtJSGRckd = _rds.Tables["TongXingDan_Title"];
                            DataRow drJSGRckd = dtJSGRckd.NewRow();
                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drJSGRckd["ChukudanBH"] = ckd.ChukudanBH == null ? "" : ckd.ChukudanBH.Trim();
                                    drJSGRckd["KehuMC"] = ckd.KehuMC == null ? "" : ckd.KehuMC.Trim();
                                    drJSGRckd["Yunsongdizhi"] = ckd.Yunsongdizhi == null ? "" : ckd.Yunsongdizhi.Trim();
                                    drJSGRckd["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drJSGRckd["Lianxiren"] = ckd.Lianxiren == null ? "" : ckd.Lianxiren.Trim();
                                    drJSGRckd["LianxiDH"] = ckd.LianxiDH == null ? "" : ckd.LianxiDH.Trim();
                                    drJSGRckd["Beizhu"] = ckd.Beizhu == null ? "" : ckd.Beizhu.Trim();
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drJSGRckd["HuozhuID"] = wtkhdata.Kehumingcheng == null ? "" : wtkhdata.Kehumingcheng.Trim();
                                    }

                                    drJSGRckd["YunsongFS"] = MvcApplication.DeliveryType[ckd.YunsongFS == null ? int.Parse("0") : (int)ckd.YunsongFS];

                                    if (ckd.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == ckd.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drJSGRckd["Kuaidi"] = "";
                                        else
                                            drJSGRckd["Kuaidi"] = tran_company.Mingcheng == null ? "" : tran_company.Mingcheng.Trim();
                                    }
                                    drJSGRckd["JiesuanFS"] = MvcApplication.SettlingType[ckd.JiesuanFS == null ? int.Parse("0") : (int)ckd.JiesuanFS];
                                }
                                drJSGRckd["tx_ChukuJSs"] = string.Format("{0:0.00}", JSGRtx_ChukuJSs);
                                drJSGRckd["tx_JianhuoSLs"] = string.Format("{0:0.00}", JSGRtx_JianhuoSLs);
                                dtJSGRckd.Rows.Add(drJSGRckd);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["TongXingDan_Title"]));
                            break;
                        case "GR_DBtongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptGR_DBTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtGR_DBtx = _rds.Tables["TongXingDan"];
                            DataRow drGR_DBtx;
                            float GR_DBtx_JianhuoSLs = 0;//数量总计
                            float GR_DBChukuJS = 0;//件数
                            float GR_DBtx_ChukuJSs = 0;//件数总计
                            try
                            {
                                var _grcyyq = "";
                                wms_chukudan _ckd001 = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (_ckd001 != null)
                                {
                                    _grcyyq = _ckd001.ChunyunYQ == null ? "" : _ckd001.ChunyunYQ.Trim();
                                }
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, true, p => p.Pihao).ToList();
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drGR_DBtx = dtGR_DBtx.NewRow();
                                    drGR_DBtx["Guige"] = _mx.Guige == null ? "" : _mx.Guige.Trim();
                                    drGR_DBtx["ShangpinMC"] = _mx.ShangpinMC == null ? "" : _mx.ShangpinMC.Trim();
                                    drGR_DBtx["Pihao"] = _mx.Pihao == null ? "" : _mx.Pihao.Trim();
                                    drGR_DBtx["Xuliema"] = _mx.Xuliema == null ? "" : _mx.Xuliema.Trim();
                                    drGR_DBtx["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drGR_DBtx["Huansuanlv"] = string.Format("{0:0.00}", _mx.Huansuanlv);
                                    drGR_DBtx["JianhuoSL"] = string.Format("{0:0.00}", _mx.JianhuoSL == null ? int.Parse("0.00") : _mx.JianhuoSL);
                                    drGR_DBtx["JibenDW"] = _mx.JibenDW == null ? "" : _mx.JibenDW.Trim();

                                    var spxxData = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == _mx.ShangpinID && p.IsDelete == false);
                                    if (spxxData != null)
                                    {
                                        drGR_DBtx["ShangpinMS"] = spxxData.ShangpinMS == null ? "" : spxxData.ShangpinMS;
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == spxxData.GongyingID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drGR_DBtx["gongYingShangMC"] = gys.Mingcheng == null ? "" : gys.Mingcheng.Trim();
                                        }
                                    }

                                    GR_DBChukuJS = _mx.JianhuoSL / _mx.Huansuanlv == null ? int.Parse("0.00") : (float)_mx.JianhuoSL / _mx.Huansuanlv;
                                    drGR_DBtx["ChukuJS"] = string.Format("{0:0.00}", GR_DBChukuJS);

                                    GR_DBtx_ChukuJSs += (long)GR_DBChukuJS;//件数总计
                                    GR_DBtx_JianhuoSLs += (long)_mx.JianhuoSL;//数量总计

                                    //wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                    //if (_ckd != null)
                                    //{
                                    //    drGR_DBtx["ChunyunYQ"] = _ckd.ChunyunYQ == null ? "" : _ckd.ChunyunYQ.Trim();
                                    //}
                                    //表头储运要求
                                    //drGR_DBtx["ChunyunYQ"] = _grcyyq;
                                    //商品储运要求
                                    drGR_DBtx["ChunyunYQ"] = spxxData.Cunchutiaojian;
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        if (_scqy.ShengchanxukeBH != null)
                                            drGR_DBtx["ShengchanxukeBH"] = _scqy.Qiyemingcheng.Trim() + "/" + _scqy.ShengchanxukeBH.Trim();
                                        else
                                            drGR_DBtx["ShengchanxukeBH"] = _scqy.Qiyemingcheng;
                                        //drGR_DBtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH == null ? "" : _scqy.ShengchanxukeBH.Trim();
                                    }
                                    else
                                        drGR_DBtx["ShengchanxukeBH"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();

                                    //注册证&备案编号2选1
                                    if (string.IsNullOrEmpty(_mx.Zhucezheng))
                                    {
                                        if (_scqy != null)
                                            drGR_DBtx["Zhucezheng_BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                        else
                                            drGR_DBtx["Zhucezheng_BeianBH"] = "";
                                    }
                                    else
                                    {
                                        drGR_DBtx["Zhucezheng_BeianBH"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();
                                    }

                                    dtGR_DBtx.Rows.Add(drGR_DBtx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            //页眉&页脚
                            DataTable dtGR_DBckd = _rds.Tables["TongXingDan_Title"];
                            DataRow drGR_DBckd = dtGR_DBckd.NewRow();
                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drGR_DBckd["ChukudanBH"] = ckd.KehuDH == null ? "" : ckd.KehuDH.Trim();
                                    drGR_DBckd["KehuMC"] = ckd.KehuMC == null ? "" : ckd.KehuMC.Trim();
                                    drGR_DBckd["Yunsongdizhi"] = ckd.Yunsongdizhi == null ? "" : ckd.Yunsongdizhi.Trim();
                                    drGR_DBckd["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drGR_DBckd["Lianxiren"] = ckd.Lianxiren == null ? "" : ckd.Lianxiren.Trim();
                                    drGR_DBckd["LianxiDH"] = ckd.LianxiDH == null ? "" : ckd.LianxiDH.Trim();
                                    drGR_DBckd["Beizhu"] = ckd.Beizhu == null ? "" : ckd.Beizhu.Trim();
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drGR_DBckd["HuozhuID"] = wtkhdata.Kehumingcheng == null ? "" : wtkhdata.Kehumingcheng.Trim();
                                    }

                                    drGR_DBckd["YunsongFS"] = MvcApplication.DeliveryType[ckd.YunsongFS == null ? int.Parse("0") : (int)ckd.YunsongFS];

                                    if (ckd.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == ckd.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drGR_DBckd["Kuaidi"] = "";
                                        else
                                            drGR_DBckd["Kuaidi"] = tran_company.Mingcheng == null ? "" : tran_company.Mingcheng.Trim();
                                    }
                                    drGR_DBckd["JiesuanFS"] = MvcApplication.SettlingType[ckd.JiesuanFS == null ? int.Parse("0") : (int)ckd.JiesuanFS];
                                }
                                drGR_DBckd["tx_ChukuJSs"] = string.Format("{0:0.00}", GR_DBtx_ChukuJSs);
                                drGR_DBckd["tx_JianhuoSLs"] = string.Format("{0:0.00}", GR_DBtx_JianhuoSLs);
                                dtGR_DBckd.Rows.Add(drGR_DBckd);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["TongXingDan_Title"]));
                            break;
                        case "WQtongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptWQTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtWQtx = _rds.Tables["TongXingDan"];
                            DataRow drWQtx;
                            float WQtx_JianhuoSLs = 0;//数量总计
                            float WQChukuJS = 0;//件数
                            float WQtx_ChukuJSs = 0;//件数总计
                            try
                            {
                                var _wqcyyq = "";
                                wms_chukudan _ckd001 = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (_ckd001 != null)
                                {
                                    _wqcyyq = _ckd001.ChunyunYQ == null ? "" : _ckd001.ChunyunYQ.Trim();
                                }
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, true, p => p.Pihao).ToList();
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drWQtx = dtWQtx.NewRow();
                                    drWQtx["Guige"] = _mx.Guige == null ? "" : _mx.Guige.Trim();
                                    drWQtx["ShangpinMC"] = _mx.ShangpinMC == null ? "" : _mx.ShangpinMC.Trim();
                                    drWQtx["Pihao"] = _mx.Pihao == null ? "" : _mx.Pihao.Trim();
                                    drWQtx["Xuliema"] = _mx.Xuliema == null ? "" : _mx.Xuliema.Trim();
                                    drWQtx["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drWQtx["Huansuanlv"] = string.Format("{0:0.00}", _mx.Huansuanlv);
                                    drWQtx["JianhuoSL"] = string.Format("{0:0.00}", _mx.JianhuoSL == null ? int.Parse("0.00") : _mx.JianhuoSL);
                                    drWQtx["JibenDW"] = _mx.JibenDW == null ? "" : _mx.JibenDW.Trim();
                                    drWQtx["Changjia"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();
                                    drWQtx["Beizhu"] = _mx.Beizhu == null ? "" : _mx.Beizhu.Trim();

                                    var spxxData = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == _mx.ShangpinID && p.IsDelete == false);
                                    if (spxxData != null)
                                    {
                                        drWQtx["ShangpinMS"] = spxxData.ShangpinMS == null ? "" : spxxData.ShangpinMS;
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == spxxData.GongyingID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drWQtx["gongYingShangMC"] = gys.Mingcheng == null ? "" : gys.Mingcheng.Trim();
                                        }
                                    }

                                    WQChukuJS = _mx.JianhuoSL / _mx.Huansuanlv == null ? int.Parse("0.00") : (float)_mx.JianhuoSL / _mx.Huansuanlv;
                                    drWQtx["ChukuJS"] = string.Format("{0:0.00}", WQChukuJS);

                                    WQtx_ChukuJSs += (long)WQChukuJS;//件数总计
                                    WQtx_JianhuoSLs += (long)_mx.JianhuoSL;//数量总计

                                    //wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                    //if (_ckd != null)
                                    //{
                                    //    drWQtx["ChunyunYQ"] = _ckd.ChunyunYQ == null ? "" : _ckd.ChunyunYQ.Trim();
                                    //}
                                    //表头储运要求
                                    //drWQtx["ChunyunYQ"] = _wqcyyq;
                                    //商品储运要求
                                    drWQtx["ChunyunYQ"] = spxxData.Cunchutiaojian;
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        if (_scqy.ShengchanxukeBH != null)
                                            drWQtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH.Trim();
                                        else
                                            drWQtx["ShengchanxukeBH"] = "";
                                    }
                                    else
                                        drWQtx["ShengchanxukeBH"] = "";

                                    //注册证&备案编号2选1
                                    if (string.IsNullOrEmpty(_mx.Zhucezheng))
                                    {
                                        if (_scqy != null)
                                            drWQtx["Zhucezheng_BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                        else
                                            drWQtx["Zhucezheng_BeianBH"] = "";
                                    }
                                    else
                                    {
                                        drWQtx["Zhucezheng_BeianBH"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();
                                    }

                                    dtWQtx.Rows.Add(drWQtx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            //页眉&页脚
                            DataTable dtWQckd = _rds.Tables["TongXingDan_Title"];
                            DataRow drWQckd = dtWQckd.NewRow();
                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drWQckd["ChukudanBH"] = ckd.ChukudanBH == null ? "" : ckd.ChukudanBH.Trim();
                                    drWQckd["KehuMC"] = ckd.KehuMC == null ? "" : ckd.KehuMC.Trim();
                                    drWQckd["Yunsongdizhi"] = ckd.Yunsongdizhi == null ? "" : ckd.Yunsongdizhi.Trim();
                                    drWQckd["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drWQckd["Lianxiren"] = ckd.Lianxiren == null ? "" : ckd.Lianxiren.Trim();
                                    drWQckd["LianxiDH"] = ckd.LianxiDH == null ? "" : ckd.LianxiDH.Trim();
                                    drWQckd["Beizhu"] = ckd.Beizhu == null ? "" : ckd.Beizhu.Trim();
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drWQckd["HuozhuID"] = wtkhdata.Kehumingcheng == null ? "" : wtkhdata.Kehumingcheng.Trim();
                                    }

                                    drWQckd["YunsongFS"] = MvcApplication.DeliveryType[ckd.YunsongFS == null ? int.Parse("0") : (int)ckd.YunsongFS];

                                    if (ckd.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == ckd.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drWQckd["Kuaidi"] = "";
                                        else
                                            drWQckd["Kuaidi"] = tran_company.Mingcheng == null ? "" : tran_company.Mingcheng.Trim();
                                    }
                                    drWQckd["JiesuanFS"] = MvcApplication.SettlingType[ckd.JiesuanFS == null ? int.Parse("0") : (int)ckd.JiesuanFS];
                                }
                                drWQckd["tx_ChukuJSs"] = string.Format("{0:0.00}", WQtx_ChukuJSs);
                                drWQckd["tx_JianhuoSLs"] = string.Format("{0:0.00}", WQtx_JianhuoSLs);
                                dtWQckd.Rows.Add(drWQckd);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["TongXingDan_Title"]));
                            break;
                        case "HQtongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptHQTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtHQtx = _rds.Tables["TongXingDan"];
                            DataRow drHQtx;
                            float HQtx_JianhuoSLs = 0;//数量总计
                            float HQChukuJS = 0;//件数
                            float HQtx_ChukuJSs = 0;//件数总计
                            try
                            {
                                var _hqcyyq = "";
                                wms_chukudan _ckd001 = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (_ckd001 != null)
                                {
                                    _hqcyyq = _ckd001.ChunyunYQ == null ? "" : _ckd001.ChunyunYQ.Trim();
                                }
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, true, p => p.Pihao).ToList();
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drHQtx = dtHQtx.NewRow();
                                    drHQtx["Guige"] = _mx.Guige == null ? "" : _mx.Guige.Trim();
                                    drHQtx["ShangpinMC"] = _mx.ShangpinMC == null ? "" : _mx.ShangpinMC.Trim();
                                    drHQtx["Pihao"] = _mx.Pihao == null ? "" : _mx.Pihao.Trim();
                                    drHQtx["Xuliema"] = _mx.Xuliema == null ? "" : _mx.Xuliema.Trim();
                                    drHQtx["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drHQtx["Huansuanlv"] = string.Format("{0:0.00}", _mx.Huansuanlv);
                                    drHQtx["JianhuoSL"] = string.Format("{0:0.00}", _mx.JianhuoSL == null ? int.Parse("0.00") : _mx.JianhuoSL);
                                    drHQtx["JibenDW"] = _mx.JibenDW == null ? "" : _mx.JibenDW.Trim();
                                    drHQtx["Changjia"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();
                                    drHQtx["Beizhu"] = _mx.Beizhu == null ? "" : _mx.Beizhu.Trim();

                                    var spxxData = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == _mx.ShangpinID && p.IsDelete == false);
                                    if (spxxData != null)
                                    {
                                        drHQtx["ShangpinMS"] = spxxData.ShangpinMS == null ? "" : spxxData.ShangpinMS;
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == spxxData.GongyingID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drHQtx["gongYingShangMC"] = gys.Mingcheng == null ? "" : gys.Mingcheng.Trim();
                                        }
                                    }

                                    HQChukuJS = _mx.JianhuoSL / _mx.Huansuanlv == null ? int.Parse("0.00") : (float)_mx.JianhuoSL / _mx.Huansuanlv;
                                    drHQtx["ChukuJS"] = string.Format("{0:0.00}", HQChukuJS);

                                    HQtx_ChukuJSs += (long)HQChukuJS;//件数总计
                                    HQtx_JianhuoSLs += (long)_mx.JianhuoSL;//数量总计

                                    //wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                    //if (_ckd != null)
                                    //{
                                    //    drHQtx["ChunyunYQ"] = _ckd.ChunyunYQ == null ? "" : _ckd.ChunyunYQ.Trim();
                                    //}
                                    //表头储运要求
                                    //drHQtx["ChunyunYQ"] = _hqcyyq;
                                    //商品储运要求
                                    drHQtx["ChunyunYQ"] = spxxData.Cunchutiaojian;
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        if (_scqy.ShengchanxukeBH != null)
                                            drHQtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH.Trim();
                                        else
                                            drHQtx["ShengchanxukeBH"] = "";
                                    }
                                    else
                                        drHQtx["ShengchanxukeBH"] = "";

                                    //注册证&备案编号2选1
                                    if (string.IsNullOrEmpty(_mx.Zhucezheng))
                                    {
                                        if (_scqy != null)
                                            drHQtx["Zhucezheng_BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                        else
                                            drHQtx["Zhucezheng_BeianBH"] = "";
                                    }
                                    else
                                    {
                                        drHQtx["Zhucezheng_BeianBH"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();
                                    }

                                    dtHQtx.Rows.Add(drHQtx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            //页眉&页脚
                            DataTable dtHQckd = _rds.Tables["TongXingDan_Title"];
                            DataRow drHQckd = dtHQckd.NewRow();
                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drHQckd["ChukudanBH"] = ckd.ChukudanBH == null ? "" : ckd.ChukudanBH.Trim();
                                    drHQckd["KehuMC"] = ckd.KehuMC == null ? "" : ckd.KehuMC.Trim();
                                    drHQckd["Yunsongdizhi"] = ckd.Yunsongdizhi == null ? "" : ckd.Yunsongdizhi.Trim();
                                    drHQckd["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drHQckd["Lianxiren"] = ckd.Lianxiren == null ? "" : ckd.Lianxiren.Trim();
                                    drHQckd["LianxiDH"] = ckd.LianxiDH == null ? "" : ckd.LianxiDH.Trim();
                                    drHQckd["Beizhu"] = ckd.Beizhu == null ? "" : ckd.Beizhu.Trim();
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drHQckd["HuozhuID"] = wtkhdata.Kehumingcheng == null ? "" : wtkhdata.Kehumingcheng.Trim();
                                    }

                                    drHQckd["YunsongFS"] = MvcApplication.DeliveryType[ckd.YunsongFS == null ? int.Parse("0") : (int)ckd.YunsongFS];

                                    if (ckd.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == ckd.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drHQckd["Kuaidi"] = "";
                                        else
                                            drHQckd["Kuaidi"] = tran_company.Mingcheng == null ? "" : tran_company.Mingcheng.Trim();
                                    }
                                    drHQckd["JiesuanFS"] = MvcApplication.SettlingType[ckd.JiesuanFS == null ? int.Parse("0") : (int)ckd.JiesuanFS];
                                }
                                drHQckd["tx_ChukuJSs"] = string.Format("{0:0.00}", HQtx_ChukuJSs);
                                drHQckd["tx_JianhuoSLs"] = string.Format("{0:0.00}", HQtx_JianhuoSLs);
                                dtHQckd.Rows.Add(drHQckd);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["TongXingDan_Title"]));
                            break;
                        case "MLtongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptMLTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtMLtx = _rds.Tables["TongXingDan"];
                            DataRow drMLtx;
                            float MLtx_JianhuoSLs = 0;//数量总计
                            float MLChukuJS = 0;//件数
                            float MLtx_ChukuJSs = 0;//件数总计
                            try
                            {
                                //var _grcyyq = "";
                                //wms_chukudan _ckd001 = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                //if (_ckd001 != null)
                                //{
                                //    _grcyyq = _ckd001.ChunyunYQ == null ? "" : _ckd001.ChunyunYQ.Trim();
                                //}
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, true, p => p.Pihao).ToList();
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drMLtx = dtMLtx.NewRow();
                                    drMLtx["Guige"] = _mx.Guige == null ? "" : _mx.Guige.Trim();
                                    drMLtx["ShangpinMC"] = _mx.ShangpinMC == null ? "" : _mx.ShangpinMC.Trim();
                                    drMLtx["Pihao"] = _mx.Pihao == null ? "" : _mx.Pihao.Trim();
                                    drMLtx["Xuliema"] = _mx.Xuliema == null ? "" : _mx.Xuliema.Trim();
                                    drMLtx["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drMLtx["Huansuanlv"] = string.Format("{0:0.00}", _mx.Huansuanlv);
                                    drMLtx["JianhuoSL"] = string.Format("{0:0.00}", _mx.JianhuoSL == null ? int.Parse("0.00") : _mx.JianhuoSL);
                                    drMLtx["JibenDW"] = _mx.JibenDW == null ? "" : _mx.JibenDW.Trim();
                                    drMLtx["Beizhu"] = _mx.Beizhu == null ? "" : _mx.Beizhu.Trim();

                                    var spxxData = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == _mx.ShangpinID && p.IsDelete == false);
                                    if (spxxData != null)
                                    {
                                        drMLtx["ShangpinMS"] = spxxData.ShangpinMS == null ? "" : spxxData.ShangpinMS;
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == spxxData.GongyingID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drMLtx["gongYingShangMC"] = gys.Mingcheng == null ? "" : gys.Mingcheng.Trim();
                                        }
                                    }

                                    MLChukuJS = _mx.JianhuoSL / _mx.Huansuanlv == null ? int.Parse("0.00") : (float)_mx.JianhuoSL / _mx.Huansuanlv;
                                    drMLtx["ChukuJS"] = string.Format("{0:0.00}", MLChukuJS);

                                    MLtx_ChukuJSs += (long)MLChukuJS;//件数总计
                                    MLtx_JianhuoSLs += (long)_mx.JianhuoSL;//数量总计

                                    //wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                    //if (_ckd != null)
                                    //{
                                    //    drMLtx["ChunyunYQ"] = _ckd.ChunyunYQ == null ? "" : _ckd.ChunyunYQ.Trim();
                                    //}

                                    //商品储运要求
                                    drMLtx["ChunyunYQ"] = spxxData.Cunchutiaojian;
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        if (_scqy.ShengchanxukeBH != null)
                                            drMLtx["ShengchanxukeBH"] = _scqy.Qiyemingcheng.Trim() + "/" + _scqy.ShengchanxukeBH.Trim();
                                        else
                                            drMLtx["ShengchanxukeBH"] = _scqy.Qiyemingcheng;
                                        //drJSGRtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH == null ? "" : _scqy.ShengchanxukeBH.Trim();
                                    }
                                    else
                                        drMLtx["ShengchanxukeBH"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();

                                    //注册证&备案编号2选1
                                    if (string.IsNullOrEmpty(_mx.Zhucezheng))
                                    {
                                        if (_scqy != null)
                                            drMLtx["Zhucezheng_BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                        else
                                            drMLtx["Zhucezheng_BeianBH"] = "";
                                    }
                                    else
                                    {
                                        drMLtx["Zhucezheng_BeianBH"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();
                                    }

                                    dtMLtx.Rows.Add(drMLtx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            //页眉&页脚
                            DataTable dtMLckd = _rds.Tables["TongXingDan_Title"];
                            DataRow drMLckd = dtMLckd.NewRow();

                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drMLckd["ChukudanBH"] = ckd.ChukudanBH == null ? "" : ckd.ChukudanBH.Trim();
                                    drMLckd["KehuMC"] = ckd.KehuMC == null ? "" : ckd.KehuMC.Trim();
                                    drMLckd["Yunsongdizhi"] = ckd.Yunsongdizhi == null ? "" : ckd.Yunsongdizhi.Trim();
                                    drMLckd["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drMLckd["Lianxiren"] = ckd.Lianxiren == null ? "" : ckd.Lianxiren.Trim();
                                    drMLckd["LianxiDH"] = ckd.LianxiDH == null ? "" : ckd.LianxiDH.Trim();
                                    drMLckd["Beizhu"] = ckd.Beizhu == null ? "" : ckd.Beizhu.Trim();
                                    drMLckd["ChunyunYQ"] = ckd.ChunyunYQ == null ? "" : ckd.ChunyunYQ.Trim();
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drMLckd["HuozhuID"] = wtkhdata.Kehumingcheng == null ? "" : wtkhdata.Kehumingcheng.Trim();
                                    }

                                    drMLckd["YunsongFS"] = MvcApplication.DeliveryType[ckd.YunsongFS == null ? int.Parse("0") : (int)ckd.YunsongFS];

                                    if (ckd.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == ckd.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drMLckd["Kuaidi"] = "";
                                        else
                                            drMLckd["Kuaidi"] = tran_company.Mingcheng == null ? "" : tran_company.Mingcheng.Trim();
                                    }
                                    drMLckd["JiesuanFS"] = MvcApplication.SettlingType[ckd.JiesuanFS == null ? int.Parse("0") : (int)ckd.JiesuanFS];
                                }
                                drMLckd["tx_ChukuJSs"] = string.Format("{0:0.00}", MLtx_ChukuJSs);
                                drMLckd["tx_JianhuoSLs"] = string.Format("{0:0.00}", MLtx_JianhuoSLs);
                                dtMLckd.Rows.Add(drMLckd);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["TongXingDan_Title"]));
                            break;

                        case "BLJGtongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptBLJGTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtBLJGtx = _rds.Tables["TongXingDan"];
                            DataRow drBLJGtx;
                            float BLJGtx_JianhuoSLs = 0;//数量总计
                            float BLJGChukuJS = 0;//件数
                            float BLJGtx_ChukuJSs = 0;//件数总计
                            try
                            {
                                //var _grcyyq = "";
                                //wms_chukudan _ckd001 = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                //if (_ckd001 != null)
                                //{
                                //    _grcyyq = _ckd001.ChunyunYQ == null ? "" : _ckd001.ChunyunYQ.Trim();
                                //}
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, true, p => p.Pihao).ToList();
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drBLJGtx = dtBLJGtx.NewRow();
                                    drBLJGtx["Guige"] = _mx.Guige == null ? "" : _mx.Guige.Trim();
                                    drBLJGtx["ShangpinMC"] = _mx.ShangpinMC == null ? "" : _mx.ShangpinMC.Trim();
                                    drBLJGtx["Pihao"] = _mx.Pihao == null ? "" : _mx.Pihao.Trim();
                                    drBLJGtx["Xuliema"] = _mx.Xuliema == null ? "" : _mx.Xuliema.Trim();
                                    drBLJGtx["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drBLJGtx["Huansuanlv"] = string.Format("{0:0.00}", _mx.Huansuanlv);
                                    drBLJGtx["JianhuoSL"] = string.Format("{0:0.00}", _mx.JianhuoSL == null ? int.Parse("0.00") : _mx.JianhuoSL);
                                    drBLJGtx["JibenDW"] = _mx.JibenDW == null ? "" : _mx.JibenDW.Trim();
                                    drBLJGtx["Beizhu"] = _mx.Beizhu == null ? "" : _mx.Beizhu.Trim();

                                    var spxxData = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == _mx.ShangpinID && p.IsDelete == false);
                                    if (spxxData != null)
                                    {
                                        drBLJGtx["ShangpinMS"] = spxxData.ShangpinMS == null ? "" : spxxData.ShangpinMS;
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == spxxData.GongyingID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drBLJGtx["gongYingShangMC"] = gys.Mingcheng == null ? "" : gys.Mingcheng.Trim();
                                        }
                                    }

                                    BLJGChukuJS = _mx.JianhuoSL / _mx.Huansuanlv == null ? int.Parse("0.00") : (float)_mx.JianhuoSL / _mx.Huansuanlv;
                                    drBLJGtx["ChukuJS"] = string.Format("{0:0.00}", BLJGChukuJS);

                                    BLJGtx_ChukuJSs += (long)BLJGChukuJS;//件数总计
                                    BLJGtx_JianhuoSLs += (long)_mx.JianhuoSL;//数量总计

                                    //wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                    //if (_ckd != null)
                                    //{
                                    //    drMLtx["ChunyunYQ"] = _ckd.ChunyunYQ == null ? "" : _ckd.ChunyunYQ.Trim();
                                    //}

                                    //商品储运要求
                                    drBLJGtx["ChunyunYQ"] = spxxData.Cunchutiaojian;
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        if (_scqy.ShengchanxukeBH != null)
                                            drBLJGtx["ShengchanxukeBH"] = _scqy.Qiyemingcheng.Trim() + "/" + _scqy.ShengchanxukeBH.Trim();
                                        else
                                            drBLJGtx["ShengchanxukeBH"] = _scqy.Qiyemingcheng;
                                        //drJSGRtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH == null ? "" : _scqy.ShengchanxukeBH.Trim();
                                    }
                                    else
                                        drBLJGtx["ShengchanxukeBH"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();

                                    //注册证&备案编号2选1
                                    if (string.IsNullOrEmpty(_mx.Zhucezheng))
                                    {
                                        if (_scqy != null)
                                            drBLJGtx["Zhucezheng_BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                        else
                                            drBLJGtx["Zhucezheng_BeianBH"] = "";
                                    }
                                    else
                                    {
                                        drBLJGtx["Zhucezheng_BeianBH"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();
                                    }

                                    dtBLJGtx.Rows.Add(drBLJGtx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            //页眉&页脚
                            DataTable dtBLJGckd = _rds.Tables["TongXingDan_Title"];
                            DataRow drBLJGckd = dtBLJGckd.NewRow();

                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drBLJGckd["ChukudanBH"] = ckd.ChukudanBH == null ? "" : ckd.ChukudanBH.Trim();
                                    drBLJGckd["KehuMC"] = ckd.KehuMC == null ? "" : ckd.KehuMC.Trim();
                                    drBLJGckd["Yunsongdizhi"] = ckd.Yunsongdizhi == null ? "" : ckd.Yunsongdizhi.Trim();
                                    drBLJGckd["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drBLJGckd["Lianxiren"] = ckd.Lianxiren == null ? "" : ckd.Lianxiren.Trim();
                                    drBLJGckd["LianxiDH"] = ckd.LianxiDH == null ? "" : ckd.LianxiDH.Trim();
                                    drBLJGckd["Beizhu"] = ckd.Beizhu == null ? "" : ckd.Beizhu.Trim();
                                    drBLJGckd["ChunyunYQ"] = ckd.ChunyunYQ == null ? "" : ckd.ChunyunYQ.Trim();
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drBLJGckd["HuozhuID"] = wtkhdata.Kehumingcheng == null ? "" : wtkhdata.Kehumingcheng.Trim();
                                    }

                                    drBLJGckd["YunsongFS"] = MvcApplication.DeliveryType[ckd.YunsongFS == null ? int.Parse("0") : (int)ckd.YunsongFS];

                                    if (ckd.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == ckd.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drBLJGckd["Kuaidi"] = "";
                                        else
                                            drBLJGckd["Kuaidi"] = tran_company.Mingcheng == null ? "" : tran_company.Mingcheng.Trim();
                                    }
                                    drBLJGckd["JiesuanFS"] = MvcApplication.SettlingType[ckd.JiesuanFS == null ? int.Parse("0") : (int)ckd.JiesuanFS];
                                }
                                drBLJGckd["tx_ChukuJSs"] = string.Format("{0:0.00}", BLJGtx_ChukuJSs);
                                drBLJGckd["tx_JianhuoSLs"] = string.Format("{0:0.00}", BLJGtx_JianhuoSLs);
                                dtBLJGckd.Rows.Add(drBLJGckd);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["TongXingDan_Title"]));
                            break;

                        case "BDLtongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptBDLTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtJSGRtx1 = _rds.Tables["TongXingDan"];
                            DataRow drJSGRtx1;
                            float JSGRtx_JianhuoSLs1 = 0;//数量总计
                            float JSGRChukuJS1 = 0;//件数
                            float JSGRtx_ChukuJSs1 = 0;//件数总计
                            try
                            {
                                var _grcyyq = "";
                                wms_chukudan _ckd001 = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (_ckd001 != null)
                                {
                                    _grcyyq = _ckd001.ChunyunYQ == null ? "" : _ckd001.ChunyunYQ.Trim();
                                }
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, true, p => p.Pihao).ToList();
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drJSGRtx1 = dtJSGRtx1.NewRow();
                                    drJSGRtx1["Guige"] = _mx.Guige == null ? "" : _mx.Guige.Trim();
                                    drJSGRtx1["ShangpinDM"] = _mx.ShangpinDM == null ? "" : _mx.ShangpinDM.Trim();
                                    drJSGRtx1["ShangpinMC"] = _mx.ShangpinMC == null ? "" : _mx.ShangpinMC.Trim();
                                    drJSGRtx1["Pihao"] = _mx.Pihao == null ? "" : _mx.Pihao.Trim();
                                    drJSGRtx1["Xuliema"] = _mx.Xuliema == null ? "" : _mx.Xuliema.Trim();
                                    drJSGRtx1["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drJSGRtx1["Huansuanlv"] = string.Format("{0:0.00}", _mx.Huansuanlv);
                                    drJSGRtx1["JianhuoSL"] = string.Format("{0:0.00}", _mx.JianhuoSL == null ? int.Parse("0.00") : _mx.JianhuoSL);
                                    drJSGRtx1["JibenDW"] = _mx.JibenDW == null ? "" : _mx.JibenDW.Trim();

                                    var spxxData = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == _mx.ShangpinID && p.IsDelete == false);
                                    if (spxxData != null)
                                    {
                                        drJSGRtx1["ShangpinMS"] = spxxData.ShangpinMS == null ? "" : spxxData.ShangpinMS;
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == spxxData.GongyingID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drJSGRtx1["gongYingShangMC"] = gys.Mingcheng == null ? "" : gys.Mingcheng.Trim();
                                        }
                                    }

                                    JSGRChukuJS1 = _mx.JianhuoSL / _mx.Huansuanlv == null ? int.Parse("0.00") : (float)_mx.JianhuoSL / _mx.Huansuanlv;
                                    drJSGRtx1["ChukuJS"] = string.Format("{0:0.00}", JSGRChukuJS1);

                                    JSGRtx_ChukuJSs1 += (long)JSGRChukuJS1;//件数总计
                                    JSGRtx_JianhuoSLs1 += (long)_mx.JianhuoSL;//数量总计

                                    //wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                    //if (_ckd != null)
                                    //{
                                    //    drJSGRtx["ChunyunYQ"] = _ckd.ChunyunYQ == null ? "" : _ckd.ChunyunYQ.Trim();
                                    //}
                                    drJSGRtx1["ChunyunYQ"] = _grcyyq;
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        if (_scqy.ShengchanxukeBH != null)
                                            drJSGRtx1["ShengchanxukeBH"] = _scqy.Qiyemingcheng.Trim() + "/" + _scqy.ShengchanxukeBH.Trim();
                                        else
                                            drJSGRtx1["ShengchanxukeBH"] = _scqy.Qiyemingcheng;
                                        //drJSGRtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH == null ? "" : _scqy.ShengchanxukeBH.Trim();
                                    }
                                    else
                                        drJSGRtx1["ShengchanxukeBH"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();

                                    //注册证&备案编号2选1
                                    if (string.IsNullOrEmpty(_mx.Zhucezheng))
                                    {
                                        if (_scqy != null)
                                            drJSGRtx1["Zhucezheng_BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                        else
                                            drJSGRtx1["Zhucezheng_BeianBH"] = "";
                                    }
                                    else
                                    {
                                        drJSGRtx1["Zhucezheng_BeianBH"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();
                                    }

                                    dtJSGRtx1.Rows.Add(drJSGRtx1);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            //页眉&页脚
                            DataTable dtJSGRckd1 = _rds.Tables["TongXingDan_Title"];
                            DataRow drJSGRckd1 = dtJSGRckd1.NewRow();
                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drJSGRckd1["ChukudanBH"] = ckd.ChukudanBH == null ? "" : ckd.ChukudanBH.Trim();
                                    drJSGRckd1["KehuMC"] = ckd.KehuMC == null ? "" : ckd.KehuMC.Trim();
                                    drJSGRckd1["Yunsongdizhi"] = ckd.Yunsongdizhi == null ? "" : ckd.Yunsongdizhi.Trim();
                                    drJSGRckd1["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drJSGRckd1["Lianxiren"] = ckd.Lianxiren == null ? "" : ckd.Lianxiren.Trim();
                                    drJSGRckd1["LianxiDH"] = ckd.LianxiDH == null ? "" : ckd.LianxiDH.Trim();
                                    drJSGRckd1["Beizhu"] = ckd.Beizhu == null ? "" : ckd.Beizhu.Trim();
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drJSGRckd1["HuozhuID"] = wtkhdata.Kehumingcheng == null ? "" : wtkhdata.Kehumingcheng.Trim();
                                    }

                                    drJSGRckd1["YunsongFS"] = MvcApplication.DeliveryType[ckd.YunsongFS == null ? int.Parse("0") : (int)ckd.YunsongFS];

                                    if (ckd.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == ckd.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drJSGRckd1["Kuaidi"] = "";
                                        else
                                            drJSGRckd1["Kuaidi"] = tran_company.Mingcheng == null ? "" : tran_company.Mingcheng.Trim();
                                    }
                                    drJSGRckd1["JiesuanFS"] = MvcApplication.SettlingType[ckd.JiesuanFS == null ? int.Parse("0") : (int)ckd.JiesuanFS];
                                }
                                drJSGRckd1["tx_ChukuJSs"] = string.Format("{0:0.00}", JSGRtx_ChukuJSs1);
                                drJSGRckd1["tx_JianhuoSLs"] = string.Format("{0:0.00}", JSGRtx_JianhuoSLs1);
                                dtJSGRckd1.Rows.Add(drJSGRckd1);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["TongXingDan_Title"]));


                            break;

                        case "BJtongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptBJTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtJSGRtx2 = _rds.Tables["TongXingDan"];
                            DataRow drJSGRtx2;
                            float JSGRtx_JianhuoSLs2 = 0;//数量总计
                            float JSGRChukuJS2 = 0;//件数
                            float JSGRtx_ChukuJSs2 = 0;//件数总计
                            try
                            {
                                var _grcyyq = "";
                                wms_chukudan _ckd001 = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (_ckd001 != null)
                                {
                                    _grcyyq = _ckd001.ChunyunYQ == null ? "" : _ckd001.ChunyunYQ.Trim();
                                }
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, true, p => p.Pihao).ToList();
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drJSGRtx2 = dtJSGRtx2.NewRow();
                                    drJSGRtx2["Guige"] = _mx.Guige == null ? "" : _mx.Guige.Trim();
                                    drJSGRtx2["ShangpinDM"] = _mx.ShangpinDM == null ? "" : _mx.ShangpinDM.Trim();
                                    drJSGRtx2["ShangpinMC"] = _mx.ShangpinMC == null ? "" : _mx.ShangpinMC.Trim();
                                    drJSGRtx2["Pihao"] = _mx.Pihao == null ? "" : _mx.Pihao.Trim();
                                    drJSGRtx2["Xuliema"] = _mx.Xuliema == null ? "" : _mx.Xuliema.Trim();
                                    drJSGRtx2["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drJSGRtx2["Huansuanlv"] = string.Format("{0:0.00}", _mx.Huansuanlv);
                                    drJSGRtx2["JianhuoSL"] = string.Format("{0:0.00}", _mx.JianhuoSL == null ? int.Parse("0.00") : _mx.JianhuoSL);
                                    drJSGRtx2["JibenDW"] = _mx.JibenDW == null ? "" : _mx.JibenDW.Trim();

                                    var spxxData = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == _mx.ShangpinID && p.IsDelete == false);
                                    if (spxxData != null)
                                    {
                                        drJSGRtx2["ShangpinMS"] = spxxData.ShangpinMS == null ? "" : spxxData.ShangpinMS;
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == spxxData.GongyingID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drJSGRtx2["gongYingShangMC"] = gys.Mingcheng == null ? "" : gys.Mingcheng.Trim();
                                        }
                                    }

                                    JSGRChukuJS2 = _mx.JianhuoSL / _mx.Huansuanlv == null ? int.Parse("0.00") : (float)_mx.JianhuoSL / _mx.Huansuanlv;
                                    drJSGRtx2["ChukuJS"] = string.Format("{0:0.00}", JSGRChukuJS2);

                                    JSGRtx_ChukuJSs2 += (long)JSGRChukuJS2;//件数总计
                                    JSGRtx_JianhuoSLs2 += (long)_mx.JianhuoSL;//数量总计

                                    //wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                    //if (_ckd != null)
                                    //{
                                    //    drJSGRtx["ChunyunYQ"] = _ckd.ChunyunYQ == null ? "" : _ckd.ChunyunYQ.Trim();
                                    //}
                                    drJSGRtx2["ChunyunYQ"] = _grcyyq;
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        if (_scqy.ShengchanxukeBH != null)
                                            drJSGRtx2["ShengchanxukeBH"] = _scqy.Qiyemingcheng.Trim() + "/" + _scqy.ShengchanxukeBH.Trim();
                                        else
                                            drJSGRtx2["ShengchanxukeBH"] = _scqy.Qiyemingcheng;
                                        //drJSGRtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH == null ? "" : _scqy.ShengchanxukeBH.Trim();
                                    }
                                    else
                                        drJSGRtx2["ShengchanxukeBH"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();

                                    //注册证&备案编号2选1
                                    if (string.IsNullOrEmpty(_mx.Zhucezheng))
                                    {
                                        if (_scqy != null)
                                            drJSGRtx2["Zhucezheng_BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                        else
                                            drJSGRtx2["Zhucezheng_BeianBH"] = "";
                                    }
                                    else
                                    {
                                        drJSGRtx2["Zhucezheng_BeianBH"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();
                                    }

                                    dtJSGRtx2.Rows.Add(drJSGRtx2);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            //页眉&页脚
                            DataTable dtJSGRckd2 = _rds.Tables["TongXingDan_Title"];
                            DataRow drJSGRckd2 = dtJSGRckd2.NewRow();
                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drJSGRckd2["ChukudanBH"] = ckd.ChukudanBH == null ? "" : ckd.ChukudanBH.Trim();
                                    drJSGRckd2["KehuMC"] = ckd.KehuMC == null ? "" : ckd.KehuMC.Trim();
                                    drJSGRckd2["Yunsongdizhi"] = ckd.Yunsongdizhi == null ? "" : ckd.Yunsongdizhi.Trim();
                                    drJSGRckd2["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drJSGRckd2["Lianxiren"] = ckd.Lianxiren == null ? "" : ckd.Lianxiren.Trim();
                                    drJSGRckd2["LianxiDH"] = ckd.LianxiDH == null ? "" : ckd.LianxiDH.Trim();
                                    drJSGRckd2["Beizhu"] = ckd.Beizhu == null ? "" : ckd.Beizhu.Trim();
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drJSGRckd2["HuozhuID"] = wtkhdata.Kehumingcheng == null ? "" : wtkhdata.Kehumingcheng.Trim();
                                    }

                                    drJSGRckd2["YunsongFS"] = MvcApplication.DeliveryType[ckd.YunsongFS == null ? int.Parse("0") : (int)ckd.YunsongFS];

                                    if (ckd.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == ckd.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drJSGRckd2["Kuaidi"] = "";
                                        else
                                            drJSGRckd2["Kuaidi"] = tran_company.Mingcheng == null ? "" : tran_company.Mingcheng.Trim();
                                    }
                                    drJSGRckd2["JiesuanFS"] = MvcApplication.SettlingType[ckd.JiesuanFS == null ? int.Parse("0") : (int)ckd.JiesuanFS];
                                }
                                drJSGRckd2["tx_ChukuJSs"] = string.Format("{0:0.00}", JSGRtx_ChukuJSs2);
                                drJSGRckd2["tx_JianhuoSLs"] = string.Format("{0:0.00}", JSGRtx_JianhuoSLs2);
                                dtJSGRckd2.Rows.Add(drJSGRckd2);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["TongXingDan_Title"]));


                            break;

                        case "YGtongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptYGTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtYGtx = _rds.Tables["TongXingDan"];
                            DataRow drYGtx;
                            float YGtx_JianhuoSLs = 0;//数量总计
                            float YGChukuJS = 0;//件数
                            float YGtx_ChukuJSs = 0;//件数总计
                            try
                            {
                                var _ygcyyq = "";
                                wms_chukudan _ckd001 = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (_ckd001 != null)
                                {
                                    _ygcyyq = _ckd001.ChunyunYQ == null ? "" : _ckd001.ChunyunYQ.Trim();
                                }
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, true, p => p.Pihao).ToList();
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drYGtx = dtYGtx.NewRow();
                                    drYGtx["Guige"] = _mx.Guige == null ? "" : _mx.Guige.Trim();
                                    drYGtx["ShangpinMC"] = _mx.ShangpinMC == null ? "" : _mx.ShangpinMC.Trim();
                                    drYGtx["Pihao"] = _mx.Pihao == null ? "" : _mx.Pihao.Trim();
                                    drYGtx["Xuliema"] = _mx.Xuliema == null ? "" : _mx.Xuliema.Trim();
                                    drYGtx["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drYGtx["Huansuanlv"] = string.Format("{0:0.00}", _mx.Huansuanlv);
                                    drYGtx["JianhuoSL"] = string.Format("{0:0.00}", _mx.JianhuoSL == null ? int.Parse("0.00") : _mx.JianhuoSL);
                                    drYGtx["JibenDW"] = _mx.JibenDW == null ? "" : _mx.JibenDW.Trim();
                                    drYGtx["Beizhu"] = _mx.Beizhu == null ? "" : _mx.Beizhu.Trim();

                                    var spxxData = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == _mx.ShangpinID && p.IsDelete == false);
                                    if (spxxData != null)
                                    {
                                        drYGtx["ShangpinMS"] = spxxData.ShangpinMS == null ? "" : spxxData.ShangpinMS;
                                        drYGtx["Cunchutiaojian"] = spxxData.Cunchutiaojian == null ? "" : spxxData.Cunchutiaojian;
                                        drYGtx["Qiyemingcheng"] = spxxData.Qiyemingcheng == null ? "" : spxxData.Qiyemingcheng;
                                        
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == spxxData.GongyingID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drYGtx["gongYingShangMC"] = gys.Mingcheng == null ? "" : gys.Mingcheng.Trim();
                                        }
                                    }

                                    YGChukuJS = _mx.JianhuoSL / _mx.Huansuanlv == null ? int.Parse("0.00") : (float)_mx.JianhuoSL / _mx.Huansuanlv;
                                    drYGtx["ChukuJS"] = string.Format("{0:0.00}", YGChukuJS);

                                    YGtx_ChukuJSs += (long)YGChukuJS;//件数总计
                                    YGtx_JianhuoSLs += (long)_mx.JianhuoSL;//数量总计

                                    //wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                    //if (_ckd != null)
                                    //{
                                    //    drJSGRtx["ChunyunYQ"] = _ckd.ChunyunYQ == null ? "" : _ckd.ChunyunYQ.Trim();
                                    //}
                                    //drYGtx["ChunyunYQ"] = _ygcyyq;
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        if (_scqy.ShengchanxukeBH != null)
                                            drYGtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH.Trim();
                                        else
                                            drYGtx["ShengchanxukeBH"] = _scqy.Qiyemingcheng;
                                        //drJSGRtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH == null ? "" : _scqy.ShengchanxukeBH.Trim();
                                        if (_scqy.BeianBH != null)
                                            drYGtx["BeianBH"] = _scqy.BeianBH.Trim();
                                        else
                                            drYGtx["BeianBH"] = _scqy.Qiyemingcheng;
                                    }
                                    else
                                    {
                                        drYGtx["ShengchanxukeBH"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();
                                        drYGtx["BeianBH"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();
                                    }

                                    //注册证&备案编号2选1
                                    //if (string.IsNullOrEmpty(_mx.Zhucezheng))
                                    //{
                                    //    if (_scqy != null)
                                    //        drYGtx["Zhucezheng_BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                    //    else
                                    //        drYGtx["Zhucezheng_BeianBH"] = "";
                                    //}
                                    //else
                                    //{
                                    //    drYGtx["Zhucezheng_BeianBH"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();
                                    //}

                                    drYGtx["Zhucezheng_BeianBH"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();

                                    dtYGtx.Rows.Add(drYGtx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            //页眉&页脚
                            DataTable dtYGckd = _rds.Tables["TongXingDan_Title"];
                            DataRow drYGckd = dtYGckd.NewRow();
                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drYGckd["ChukudanBH"] = ckd.ChukudanBH == null ? "" : ckd.ChukudanBH.Trim();
                                    drYGckd["KehuMC"] = ckd.KehuMC == null ? "" : ckd.KehuMC.Trim();
                                    drYGckd["Yunsongdizhi"] = ckd.Yunsongdizhi == null ? "" : ckd.Yunsongdizhi.Trim();
                                    drYGckd["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drYGckd["Lianxiren"] = ckd.Lianxiren == null ? "" : ckd.Lianxiren.Trim();
                                    drYGckd["LianxiDH"] = ckd.LianxiDH == null ? "" : ckd.LianxiDH.Trim();
                                    drYGckd["Beizhu"] = ckd.Beizhu == null ? "" : ckd.Beizhu.Trim();
                                    drYGckd["ChunyunYQ"] = ckd.ChunyunYQ == null ? "" : ckd.ChunyunYQ.Trim();
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drYGckd["HuozhuID"] = wtkhdata.Kehumingcheng == null ? "" : wtkhdata.Kehumingcheng.Trim();
                                    }

                                    drYGckd["YunsongFS"] = MvcApplication.DeliveryType[ckd.YunsongFS == null ? int.Parse("0") : (int)ckd.YunsongFS];

                                    if (ckd.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == ckd.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drYGckd["Kuaidi"] = "";
                                        else
                                            drYGckd["Kuaidi"] = tran_company.Mingcheng == null ? "" : tran_company.Mingcheng.Trim();
                                    }
                                    drYGckd["JiesuanFS"] = MvcApplication.SettlingType[ckd.JiesuanFS == null ? int.Parse("0") : (int)ckd.JiesuanFS];
                                }
                                drYGckd["tx_ChukuJSs"] = string.Format("{0:0.00}", YGtx_ChukuJSs);
                                drYGckd["tx_JianhuoSLs"] = string.Format("{0:0.00}", YGtx_JianhuoSLs);
                                dtYGckd.Rows.Add(drYGckd);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["TongXingDan_Title"]));
                            break;

                        case "JSWZtongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptJSWZTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtJSWZTx = _rds.Tables["JSWZ_TongXingDan"];
                            DataRow drJSWZTx;
                            try
                            {
                                var _wzcyyq = "";
                                wms_chukudan _ckd002 = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_JSWZ_TXD_id) && p.IsDelete == false);
                                if (_ckd002 != null)
                                {
                                    _wzcyyq = _ckd002.ChunyunYQ == null ? "" : _ckd002.ChunyunYQ.Trim();
                                }
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_JSWZ_TXD_id) && p.IsDelete == false, true, p => p.Pihao).ToList();
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drJSWZTx = dtJSWZTx.NewRow();
                                    drJSWZTx["Guige"] = _mx.Guige == null ? "" : _mx.Guige.Trim();
                                    drJSWZTx["ShangpinMC"] = _mx.ShangpinMC == null ? "" : _mx.ShangpinMC.Trim();
                                    drJSWZTx["Pihao"] = _mx.Pihao == null ? "" : _mx.Pihao.Trim();
                                    //嘉事唯纵需要获取出库序列码
                                    var _xlms = "";
                                    var xlms = ServiceFactory.wms_chukuxlmservice.LoadSortEntities(p => p.ChukuID == _mx.ChukuID && p.IsDelete == false && p.Pihao == _mx.Pihao, false, p => p.Pihao).ToList<wms_chukuxlm>();
                                    foreach (wms_chukuxlm xlm in xlms)
                                    {
                                        if (!string.IsNullOrEmpty(xlm.Xuliema))
                                        {
                                            _xlms = _xlms + xlm.Xuliema + ",";
                                        }
                                    }
                                    drJSWZTx["Xuliema"] = _xlms;
                                    drJSWZTx["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drJSWZTx["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShengchanRQ == null ? "" : ((DateTime)_mx.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drJSWZTx["Huansuanlv"] = string.Format("{0:0.00}", _mx.Huansuanlv);
                                    drJSWZTx["JianhuoSL"] = string.Format("{0:0.00}", _mx.JianhuoSL == null ? int.Parse("0.00") : _mx.JianhuoSL);
                                    drJSWZTx["JibenDW"] = _mx.JibenDW == null ? "" : _mx.JibenDW.Trim();
                                    drJSWZTx["Chandi"] = _mx.Chandi == null ? "" : _mx.Chandi.Trim();

                                    var spxxData = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == _mx.ShangpinID && p.IsDelete == false);
                                    if (spxxData != null)
                                    {
                                        drJSWZTx["ShangpinMS"] = spxxData.ShangpinMS == null ? "" : spxxData.ShangpinMS;
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == spxxData.GongyingID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drJSWZTx["gongYingShangMC"] = gys.Mingcheng == null ? "" : gys.Mingcheng.Trim();
                                        }
                                    }

                                    //JSGRChukuJS = _mx.JianhuoSL / _mx.Huansuanlv == null ? int.Parse("0.00") : (float)_mx.JianhuoSL / _mx.Huansuanlv;
                                    //drJSWZTx["ChukuJS"] = string.Format("{0:0.00}", JSGRChukuJS);

                                    //wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_JSWZ_TXD_id) && p.IsDelete == false);
                                    //if (_ckd != null)
                                    //{
                                    //    drJSWZTx["ChunyunYQ"] = _ckd.ChunyunYQ == null ? "" : _ckd.ChunyunYQ.Trim();
                                    //}
                                    drJSWZTx["ChunyunYQ"] =_wzcyyq;
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        if (_scqy.ShengchanxukeBH != null)
                                            drJSWZTx["ShengchanxukeBH"] = _scqy.Qiyemingcheng.Trim() + "/" + _scqy.ShengchanxukeBH.Trim();
                                        else
                                            drJSWZTx["ShengchanxukeBH"] = _scqy.Qiyemingcheng;
                                    }
                                    //注册证&备案编号2选1
                                    if (string.IsNullOrEmpty(_mx.Zhucezheng))
                                    {
                                        if (_scqy != null)
                                            drJSWZTx["Zhucezheng_BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                        else
                                            drJSWZTx["Zhucezheng_BeianBH"] = "";
                                    }
                                    else
                                    {
                                        drJSWZTx["Zhucezheng_BeianBH"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();
                                    }

                                    dtJSWZTx.Rows.Add(drJSWZTx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["JSWZ_TongXingDan"]));

                            //页眉&页脚
                            DataTable dtJSWZckd = _rds.Tables["JSWZ_TongXingDan_Title"];
                            DataRow drJSWZckd = dtJSWZckd.NewRow();
                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_JSWZ_TXD_id) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drJSWZckd["ChukudanBH"] = ckd.ChukudanBH == null ? "" : ckd.ChukudanBH.Trim();
                                    drJSWZckd["KehuMC"] = ckd.KehuMC == null ? "" : ckd.KehuMC.Trim();
                                    drJSWZckd["Yunsongdizhi"] = ckd.Yunsongdizhi == null ? "" : ckd.Yunsongdizhi.Trim();
                                    drJSWZckd["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drJSWZckd["Lianxiren"] = ckd.Lianxiren == null ? "" : ckd.Lianxiren.Trim();
                                    drJSWZckd["LianxiDH"] = ckd.LianxiDH == null ? "" : ckd.LianxiDH.Trim();
                                    drJSWZckd["Beizhu"] = ckd.Beizhu == null ? "" : ckd.Beizhu.Trim();
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drJSWZckd["HuozhuID"] = wtkhdata.Kehumingcheng == null ? "" : wtkhdata.Kehumingcheng.Trim();
                                    }

                                    drJSWZckd["YunsongFS"] = MvcApplication.DeliveryType[ckd.YunsongFS == null ? int.Parse("0") : (int)ckd.YunsongFS];

                                    if (ckd.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == ckd.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drJSWZckd["Kuaidi"] = "";
                                        else
                                            drJSWZckd["Kuaidi"] = tran_company.Mingcheng == null ? "" : tran_company.Mingcheng.Trim();
                                    }
                                    drJSWZckd["JiesuanFS"] = MvcApplication.SettlingType[ckd.JiesuanFS == null ? int.Parse("0") : (int)ckd.JiesuanFS];
                                }
                                //drJSGRckd["tx_ChukuJSs"] = string.Format("{0:0.00}", JSGRtx_ChukuJSs);
                                //drJSGRckd["tx_JianhuoSLs"] = string.Format("{0:0.00}", JSGRtx_JianhuoSLs);
                                dtJSWZckd.Rows.Add(drJSWZckd);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["JSWZ_TongXingDan_Title"]));
                            break;
                        case "MYSMtongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptMYSMTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtMYSMtx = _rds.Tables["TongXingDan"];
                            DataRow drMYSMtx;
                            float MYSMtx_JianhuoSLs = 0;//数量总计
                            float MYSMChukuJS = 0;//件数
                            float MYSMtx_ChukuJSs = 0;//件数总计
                            var mysmchucun = "";
                            try
                            {                            
                                wms_chukudan _ckd001 = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (_ckd001 != null)
                                {
                                    mysmchucun = _ckd001.ChunyunYQ == null ? "" : _ckd001.ChunyunYQ.Trim();
                                }
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, true, p => p.Pihao).ToList();
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drMYSMtx = dtMYSMtx.NewRow();
                                    drMYSMtx["Guige"] = _mx.Guige == null ? "" : _mx.Guige.Trim();
                                    drMYSMtx["ShangpinMC"] = _mx.ShangpinMC == null ? "" : _mx.ShangpinMC.Trim();
                                    drMYSMtx["Pihao"] = _mx.Pihao == null ? "" : _mx.Pihao.Trim();
                                    drMYSMtx["Xuliema"] = _mx.Xuliema == null ? "" : _mx.Xuliema.Trim();
                                    drMYSMtx["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drMYSMtx["Huansuanlv"] = string.Format("{0:0.00}", _mx.Huansuanlv);
                                    drMYSMtx["JianhuoSL"] = string.Format("{0:0.00}", _mx.JianhuoSL == null ? int.Parse("0.00") : _mx.JianhuoSL);
                                    drMYSMtx["JibenDW"] = _mx.JibenDW == null ? "" : _mx.JibenDW.Trim();

                                    var spxxData = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == _mx.ShangpinID && p.IsDelete == false);
                                    if (spxxData != null)
                                    {
                                        drMYSMtx["ShangpinMS"] = spxxData.ShangpinMS == null ? "" : spxxData.ShangpinMS;
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == spxxData.GongyingID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drMYSMtx["gongYingShangMC"] = gys.Mingcheng == null ? "" : gys.Mingcheng.Trim();
                                        }
                                    }

                                    MYSMChukuJS = _mx.JianhuoSL / _mx.Huansuanlv == null ? int.Parse("0.00") : (float)_mx.JianhuoSL / _mx.Huansuanlv;
                                    drMYSMtx["ChukuJS"] = string.Format("{0:0.00}", MYSMChukuJS);

                                    MYSMtx_ChukuJSs += (long)MYSMChukuJS;//件数总计
                                    MYSMtx_JianhuoSLs += (long)_mx.JianhuoSL;//数量总计

                                    //wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                    //if (_ckd != null)
                                    //{
                                    //    drMYSMtx["ChunyunYQ"] = _ckd.ChunyunYQ == null ? "" : _ckd.ChunyunYQ.Trim();
                                    //}
                                    //表头储运要求
                                    //drMYSMtx["ChunyunYQ"] = _grcyyq;
                                    //商品储运要求
                                    drMYSMtx["ChunyunYQ"] = spxxData.Cunchutiaojian;
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        if (_scqy.ShengchanxukeBH != null)
                                            drMYSMtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH.Trim();
                                        else
                                            drMYSMtx["ShengchanxukeBH"] = "";
                                        drMYSMtx["Qiyemingcheng"] = _scqy.Qiyemingcheng.Trim();
                                        if (_scqy.BeianBH != null)
                                            drMYSMtx["BeianBH"] = _scqy.BeianBH.Trim();
                                        else
                                            drMYSMtx["BeianBH"] = "";
                                        //drMYSMtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH == null ? "" : _scqy.ShengchanxukeBH.Trim();
                                    }
                                    else
                                        drMYSMtx["ShengchanxukeBH"] = _mx.Changjia == null ? "" : _mx.Changjia.Trim();

                                    //注册证&备案编号2选1
                                    if (string.IsNullOrEmpty(_mx.Zhucezheng))
                                    {
                                        if (_scqy != null)
                                            drMYSMtx["Zhucezheng_BeianBH"] = _scqy.BeianBH == null ? "" : _scqy.BeianBH.Trim();
                                        else
                                            drMYSMtx["Zhucezheng_BeianBH"] = "";
                                    }
                                    else
                                    {
                                        drMYSMtx["Zhucezheng_BeianBH"] = _mx.Zhucezheng == null ? "" : _mx.Zhucezheng.Trim();
                                    }

                                    dtMYSMtx.Rows.Add(drMYSMtx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));

                            //页眉&页脚
                            DataTable dtMYSMckd = _rds.Tables["TongXingDan_Title"];
                            DataRow drMYSMckd = dtMYSMckd.NewRow();
                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drMYSMckd["ChukudanBH"] = ckd.ChukudanBH == null ? "" : ckd.ChukudanBH.Trim();
                                    drMYSMckd["KehuMC"] = ckd.KehuMC == null ? "" : ckd.KehuMC.Trim();
                                    drMYSMckd["Yunsongdizhi"] = ckd.Yunsongdizhi == null ? "" : ckd.Yunsongdizhi.Trim();
                                    drMYSMckd["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drMYSMckd["Lianxiren"] = ckd.Lianxiren == null ? "" : ckd.Lianxiren.Trim();
                                    drMYSMckd["LianxiDH"] = ckd.LianxiDH == null ? "" : ckd.LianxiDH.Trim();
                                    drMYSMckd["Beizhu"] = ckd.Beizhu == null ? "" : ckd.Beizhu.Trim();
                                    drMYSMckd["Col1"] = ckd.Col1 == null ? "" : ckd.Col1.Trim();
                                    drMYSMckd["Col2"] = ckd.Col2 == null ? "" : ckd.Col2.Trim();
                                    //表头储运要求
                                    drMYSMckd["ChunyunYQ"] = mysmchucun;
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drMYSMckd["HuozhuID"] = wtkhdata.Kehumingcheng == null ? "" : wtkhdata.Kehumingcheng.Trim();
                                    }

                                    drMYSMckd["YunsongFS"] = MvcApplication.DeliveryType[ckd.YunsongFS == null ? int.Parse("0") : (int)ckd.YunsongFS];

                                    if (ckd.Kuaidi != null)
                                    {
                                        var tran_company = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.ID == ckd.Kuaidi && p.IsDelete == false);
                                        if (tran_company == null)
                                            drMYSMckd["Kuaidi"] = "";
                                        else
                                            drMYSMckd["Kuaidi"] = tran_company.Mingcheng == null ? "" : tran_company.Mingcheng.Trim();
                                    }
                                    drMYSMckd["JiesuanFS"] = MvcApplication.SettlingType[ckd.JiesuanFS == null ? int.Parse("0") : (int)ckd.JiesuanFS];
                                }
                                drMYSMckd["tx_ChukuJSs"] = string.Format("{0:0.00}", MYSMtx_ChukuJSs);
                                drMYSMckd["tx_JianhuoSLs"] = string.Format("{0:0.00}", MYSMtx_JianhuoSLs);
                                dtMYSMckd.Rows.Add(drMYSMckd);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["TongXingDan_Title"]));
                            break;
                        case "CKFuheBaoGaoDan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptCKFuheDan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtfh = _rds.Tables["CKFuheBaoGao"];
                            DataRow drfh;
                            long ckfhbg_JianhuoSLs = 0;
                            long ckfhbg_FuheSLs = 0;
                            try
                            {
                                var _fhs = ServiceFactory.quan_chukufhservice.GetOutcheckByCK(int.Parse(_outid), p => p.JianhuoSL > 0).ToList<quan_outcheck_v>();
                                foreach (quan_outcheck_v _fh in _fhs)
                                {
                                    drfh = dtfh.NewRow();
                                    drfh["ShangpinMC"] = _fh.ShangpinMC;
                                    drfh["Zhucezheng"] = _fh.Zhucezheng;
                                    drfh["Guige"] = _fh.Guige;
                                    drfh["Pihao"] = _fh.Pihao;
                                    drfh["Pihao1"] = _fh.Pihao1;
                                    drfh["Xuliema"] = _fh.Xuliema;
                                    drfh["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _fh.ShengchanRQ == null ? "" : ((DateTime)_fh.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drfh["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _fh.ShixiaoRQ == null ? "" : ((DateTime)_fh.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drfh["JianhuoSL"] = _fh.JianhuoSL;
                                    drfh["Changjia"] = _fh.Changjia;
                                    drfh["Chandi"] = _fh.Chandi;
                                    drfh["FuheSL"] = _fh.FuheSL == null ? float.Parse("0") : _fh.FuheSL;
                                    if (_fh.Fuhe == null)
                                    {
                                        drfh["Fuhe"] = "未复核";
                                    }
                                    else
                                    {
                                        drfh["Fuhe"] = MvcApplication.CheckResult[(int)_fh.Fuhe];
                                    }
                                    drfh["Fuheren"] = _fh.Fuheren == null ? "" : _fh.Fuheren;
                                    drfh["FuheSM"] = _fh.FuheSM == null ? "" : _fh.FuheSM;
                                    drfh["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _fh.MakeDate == null ? "" : ((DateTime)_fh.MakeDate).ToString("yyyy/MM/dd"));
                                    ckfhbg_JianhuoSLs += (long)_fh.JianhuoSL;
                                    if (_fh.FuheSL != null)
                                        ckfhbg_FuheSLs += (long)_fh.FuheSL;

                                    dtfh.Rows.Add(drfh);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["CKFuheBaoGao"]));

                            DataTable dtckfh = _rds.Tables["CKFuheBaoGao_Title"];
                            DataRow drckfh = dtckfh.NewRow();
                            wms_chukudan _ckfhds = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                            if (_ckfhds != null)
                            {
                                drckfh["ChukudanBH"] = _ckfhds.ChukudanBH;
                                drckfh["Yunsongdizhi"] = _ckfhds.Yunsongdizhi;
                                drckfh["Beizhu"] = _ckfhds.Beizhu;
                                drckfh["KehuMC"] = _ckfhds.KehuMC;
                                drckfh["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", _ckfhds.ChukuRQ == null ? "" : ((DateTime)_ckfhds.ChukuRQ).ToString("yyyy/MM/dd"));
                                drckfh["Lianxiren"] = _ckfhds.Lianxiren;
                                drckfh["LianxiDH"] = _ckfhds.LianxiDH;
                                base_weituokehu fh_wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == _ckfhds.HuozhuID && p.IsDelete == false);
                                if (fh_wtkhdata != null)
                                {
                                    drckfh["HuozhuID"] = fh_wtkhdata.Kehumingcheng;
                                }
                                drckfh["ckfhbgjhSLs"] = ckfhbg_JianhuoSLs;
                                drckfh["ckfhbgfhSLs"] = ckfhbg_FuheSLs;
                                dtckfh.Rows.Add(drckfh);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["CKFuheBaoGao_Title"]));
                            break;
                        case "CKFuhejianyan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptCKFuheJYdan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtfhjy = _rds.Tables["CKFuheJilu"];
                            DataRow drfhjy;
                            long ckfujy_JianhuoSLs = 0;
                            try
                            {
                                var _fhjys = ServiceFactory.quan_chukufhservice.GetOutcheckByCK(int.Parse(_outid), p => p.JianhuoSL > 0).ToList<quan_outcheck_v>();
                                foreach (quan_outcheck_v _fh in _fhjys)
                                {
                                    drfhjy = dtfhjy.NewRow();
                                    drfhjy["ShangpinMC"] = _fh.ShangpinMC;
                                    drfhjy["Zhucezheng"] = _fh.Zhucezheng;
                                    drfhjy["Guige"] = _fh.Guige;
                                    drfhjy["Pihao"] = _fh.Pihao;
                                    drfhjy["Pihao1"] = _fh.Pihao1;
                                    drfhjy["Xuliema"] = _fh.Xuliema;
                                    drfhjy["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _fh.ShengchanRQ == null ? "" : ((DateTime)_fh.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drfhjy["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _fh.ShixiaoRQ == null ? "" : ((DateTime)_fh.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drfhjy["JianhuoSL"] = _fh.JianhuoSL;
                                    drfhjy["Changjia"] = _fh.Changjia;
                                    drfhjy["Chandi"] = _fh.Chandi;
                                    ckfujy_JianhuoSLs += (long)_fh.JianhuoSL;
                                    dtfhjy.Rows.Add(drfhjy);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["CKFuheJilu"]));

                            DataTable dtckfhjy = _rds.Tables["CKFuheJilu_Title"];
                            DataRow drckfhjy = dtckfhjy.NewRow();
                            try
                            {
                                wms_chukudan _ckfhjys = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (_ckfhjys != null)
                                {
                                    drckfhjy["ChukudanBH"] = _ckfhjys.ChukudanBH;
                                    drckfhjy["Yunsongdizhi"] = _ckfhjys.Yunsongdizhi;
                                    drckfhjy["Beizhu"] = _ckfhjys.Beizhu;
                                    drckfhjy["KehuMC"] = _ckfhjys.KehuMC;
                                    drckfhjy["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", _ckfhjys.ChukuRQ == null ? "" : ((DateTime)_ckfhjys.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drckfhjy["Lianxiren"] = _ckfhjys.Lianxiren;
                                    drckfhjy["LianxiDH"] = _ckfhjys.LianxiDH;
                                }
                                base_weituokehu fhjy_wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == _ckfhjys.HuozhuID && p.IsDelete == false);
                                if (fhjy_wtkhdata != null)
                                {
                                    drckfhjy["HuozhuID"] = fhjy_wtkhdata.Kehumingcheng;
                                }
                                drckfhjy["ckfhjyjhSLs"] = ckfujy_JianhuoSLs;
                                dtckfhjy.Rows.Add(drckfhjy);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["CKFuheJilu_Title"]));
                            break;
                        case "YanShouBaoGao":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptRKyanshouBGdan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtzlysbg = _rds.Tables["RKYanshouBaoGao"];
                            DataRow drzlysbg;
                            long rkysbg_Shuliangs = 0;
                            long rkysbg_YanshouSL_Y = 0;
                            long rkysbg_YanshouSL_N = 0;
                            try
                            {
                                var _zlysbgs = ServiceFactory.quan_rukuysservice.GetEntrycheckByRK(int.Parse(_rkysid)).ToList<quan_entrycheck_v>();
                                foreach (quan_entrycheck_v _pr in _zlysbgs)
                                {
                                    drzlysbg = dtzlysbg.NewRow();
                                    //供应商
                                    wms_rukudan rkd = ServiceFactory.wms_rukudanservice.GetEntityById(s => s.ID == int.Parse(_rkysid));
                                    if (rkd != null)
                                    {
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == rkd.GongyingshangID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drzlysbg["GYSMingcheng"] = gys.Mingcheng;
                                        }
                                        drzlysbg["ChunyunYQ"] = rkd.ChunyunYQ;
                                    }
                                    drzlysbg["ystime"] = string.Format("{0:yyyy/MM/dd}", _pr.ystime == null ? "" : ((DateTime)_pr.ystime).ToString("yyyy/MM/dd"));
                                    drzlysbg["Changjia"] = _pr.Changjia;
                                    drzlysbg["ShangpinMC"] = _pr.ShangpinMC;
                                    drzlysbg["Guige"] = _pr.Guige;
                                    drzlysbg["Pihao"] = _pr.Pihao;
                                    drzlysbg["Xuliema"] = _pr.Xuliema;
                                    drzlysbg["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ == null ? "" : ((DateTime)_pr.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drzlysbg["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ == null ? "" : ((DateTime)_pr.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drzlysbg["Zhucezheng"] = _pr.Zhucezheng;
                                    drzlysbg["Shuliang"] = _pr.Shuliang;
                                    rkysbg_Shuliangs += (long)_pr.Shuliang;
                                    //验收结果
                                    if (_pr.ysresult == null)
                                    {
                                        drzlysbg["ysresult"] = "未验收";
                                        drzlysbg["YanshouHGSL"] = "0";
                                        drzlysbg["YanshouBHGSL"] = "0";
                                    }
                                    else
                                    {
                                        drzlysbg["ysresult"] = MvcApplication.CheckResult[(int)_pr.ysresult];
                                        var ysresult = MvcApplication.CheckResult[(int)_pr.ysresult];
                                        if (ysresult == "合格")
                                        {
                                            drzlysbg["YanshouHGSL"] = _pr.YanshouSL;
                                            drzlysbg["YanshouBHGSL"] = "0";
                                            rkysbg_YanshouSL_Y += (long)_pr.YanshouSL;
                                        }
                                        if (ysresult == "不合格")
                                        {
                                            drzlysbg["YanshouHGSL"] = "0";
                                            drzlysbg["YanshouBHGSL"] = _pr.YanshouSL;
                                            rkysbg_YanshouSL_N += (long)_pr.YanshouSL;
                                        }
                                    }
                                    dtzlysbg.Rows.Add(drzlysbg);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["RKYanshouBaoGao"]));

                            DataTable dtrkfhjy = _rds.Tables["RKYanshouBaoGao_Title"];
                            DataRow drrkfhjy = dtrkfhjy.NewRow();
                            try
                            {
                                wms_rukudan rkd_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkysid) && p.IsDelete == false);
                                if (rkd_others != null)
                                {
                                    base_weituokehu wtkh_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == rkd_others.HuozhuID);
                                    drrkfhjy["HuozhuID"] = wtkh_others.Kehumingcheng;
                                }
                                drrkfhjy["MakeDate"] = string.Format("{0:yyyy/MM/dd}", rkd_others.RukuRQ == null ? "" : ((DateTime)rkd_others.RukuRQ).ToString("yyyy/MM/dd"));

                                var _zlysbgs = ServiceFactory.quan_rukuysservice.GetEntrycheckByRK(int.Parse(_rkysid)).ToList<quan_entrycheck_v>();
                                //取验收人
                                var Yanshouren_first = "";
                                foreach (var rkys in _zlysbgs)
                                {
                                    var Yanshouren = rkys.Yanshouren;
                                    if (Yanshouren != null)
                                    {
                                        Yanshouren_first = Yanshouren;
                                        break;
                                    }
                                }
                                drrkfhjy["MakeMan"] = Yanshouren_first;


                                drrkfhjy["rkysbgSLs"] = rkysbg_Shuliangs;
                                drrkfhjy["rkysbgYs"] = rkysbg_YanshouSL_Y;
                                drrkfhjy["rkysbgNs"] = rkysbg_YanshouSL_N;

                                dtrkfhjy.Rows.Add(drrkfhjy);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["RKYanshouBaoGao_Title"]));
                            break;
                        case "YanShouJiLu":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptRKYanShouJiLu.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtrkysjl = _rds.Tables["RKYanshouJilu"];
                            DataRow drrkysjl;
                            //long ysjl_Shuliangs = 0;
                            float ysjl_JianShus = 0;
                            try
                            {
                                var _rkysjls = ServiceFactory.quan_rukuysservice.GetEntrycheckByRK(int.Parse(_rkysid)).ToList<quan_entrycheck_v>();
                                foreach (quan_entrycheck_v _pr in _rkysjls)
                                {
                                    drrkysjl = dtrkysjl.NewRow();
                                    //供应商
                                    wms_rukudan rkd = ServiceFactory.wms_rukudanservice.GetEntityById(s => s.ID == int.Parse(_rkysid));
                                    if (rkd != null)
                                    {
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == rkd.GongyingshangID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drrkysjl["GYSMingcheng"] = gys.Mingcheng;
                                        }
                                    }
                                    drrkysjl["Changjia"] = _pr.Changjia;
                                    drrkysjl["ShangpinMC"] = _pr.ShangpinMC;
                                    drrkysjl["Guige"] = _pr.Guige;
                                    drrkysjl["Pihao"] = _pr.Pihao;
                                    drrkysjl["Xuliema"] = _pr.Xuliema;
                                    drrkysjl["ystime"] = string.Format("{0:yyyy/MM/dd}", rkd.RukuRQ == null ? "" : ((DateTime)rkd.RukuRQ).ToString("yyyy/MM/dd"));
                                    drrkysjl["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ == null ? "" : ((DateTime)_pr.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drrkysjl["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ == null ? "" : ((DateTime)_pr.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drrkysjl["Zhucezheng"] = _pr.Zhucezheng;
                                    drrkysjl["ChunyunYQ"] = rkd.ChunyunYQ;

                                    var ysjl_JianShu = _pr.Shuliang / _pr.Huansuanlv == null ? int.Parse("0") : (float)_pr.Shuliang / _pr.Huansuanlv;
                                    drrkysjl["JianShu"] = Math.Round(ysjl_JianShu, 2);
                                    ysjl_JianShus += ysjl_JianShu;

                                    dtrkysjl.Rows.Add(drrkysjl);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["RKYanshouJilu"]));

                            DataTable dtysjl = _rds.Tables["RKYanshouJilu_Title"];
                            DataRow drysjl = dtysjl.NewRow();
                            try
                            {
                                wms_rukudan rkjl_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkysid) && p.IsDelete == false);
                                if (rkjl_others != null)
                                {
                                    base_weituokehu wtkhjl_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == rkjl_others.HuozhuID && p.IsDelete == false);
                                    if (wtkhjl_others != null)
                                    {
                                        drysjl["HuozhuID"] = wtkhjl_others.Kehumingcheng;
                                    }
                                }
                                drysjl["rkysjlJSs"] = Math.Round(ysjl_JianShus, 2);
                                dtysjl.Rows.Add(drysjl);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["RKYanshouJilu_Title"]));
                            break;
                        case "RuKuXiangDan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptShouHuoList.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtshlist = _rds.Tables["RuKuXiangDan"];
                            DataRow drshlist;
                            long rkxd_Shuliangs = 0;
                            try
                            {
                                var _shlist = ServiceFactory.wms_cunhuoservice.GetUploadList(int.Parse(_rkxdid)).ToList<wms_upload_v>();
                                foreach (wms_upload_v _sh in _shlist)
                                {
                                    drshlist = dtshlist.NewRow();
                                    drshlist["ShangpinMC"] = _sh.ShangpinMC;
                                    drshlist["Zhucezheng"] = _sh.Zhucezheng;
                                    drshlist["Guige"] = _sh.Guige;
                                    drshlist["Pihao"] = _sh.Pihao;
                                    drshlist["Pihao1"] = _sh.Pihao1;
                                    drshlist["Xuliema"] = _sh.Xuliema;
                                    drshlist["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _sh.ShengchanRQ == null ? "" : ((DateTime)_sh.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drshlist["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _sh.ShixiaoRQ == null ? "" : ((DateTime)_sh.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drshlist["Changjia"] = _sh.Changjia;
                                    drshlist["Chandi"] = _sh.Chandi;
                                    if (_sh.sshuliang == null)
                                        _sh.sshuliang = 0;
                                    drshlist["Shuliang"] = _sh.sshuliang;
                                    drshlist["Kuwei"] = _sh.Kuwei;
                                    drshlist["CunhuoSM"] = _sh.CunhuoSM;
                                    drshlist["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _sh.MakeDate == null ? "" : ((DateTime)_sh.MakeDate).ToString("yyyy/MM/dd"));
                                    rkxd_Shuliangs += (long)_sh.sshuliang;

                                    dtshlist.Rows.Add(drshlist);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }


                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["RuKuXiangDan"]));

                            DataTable dtshlist_others = _rds.Tables["RuKuXiangDan_Title"];
                            DataRow drshlist_others = dtshlist_others.NewRow();
                            try
                            {
                                wms_rukudan rkshlist_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkxdid) && p.IsDelete == false);
                                if (rkshlist_others != null)
                                {
                                    drshlist_others["RukudanBH"] = rkshlist_others.RukudanBH;
                                    drshlist_others["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", rkshlist_others.RukuRQ == null ? "" : ((DateTime)rkshlist_others.RukuRQ).ToString("yyyy/MM/dd"));
                                    drshlist_others["ChunyunYQ"] = rkshlist_others.ChunyunYQ;
                                    base_weituokehu wtkhshlist_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == rkshlist_others.HuozhuID && p.IsDelete == false);
                                    if (wtkhshlist_others != null)
                                    {
                                        drshlist_others["HuozhuID"] = wtkhshlist_others.Kehumingcheng;
                                    }
                                }
                                drshlist_others["rkxdSLs"] = rkxd_Shuliangs;

                                dtshlist_others.Rows.Add(drshlist_others);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["RuKuXiangDan_Title"]));
                            break;
                        case "ShangJiaList":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptShangJiaList.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtsjlist = _rds.Tables["ShangJiaList"];
                            DataRow drsjlist;
                            long sjl_sshuliangs = 0;
                            try
                            {
                                var _sjlist = ServiceFactory.wms_cunhuoservice.GetUploadList(int.Parse(_sjdid)).OrderBy(p => p.Kuwei).ThenBy(p => p.Pihao).ToList<wms_upload_v>();
                                foreach (wms_upload_v _sh in _sjlist)
                                {
                                    drsjlist = dtsjlist.NewRow();
                                    drsjlist["ShangpinMC"] = _sh.ShangpinMC;
                                    drsjlist["Guige"] = _sh.Guige;
                                    drsjlist["Pihao"] = _sh.Pihao;
                                    drsjlist["Pihao1"] = _sh.Pihao1;
                                    drsjlist["Xuliema"] = _sh.Xuliema;
                                    drsjlist["Shuliang"] = _sh.sshuliang == null ? float.Parse("0") : _sh.sshuliang;
                                    drsjlist["Kuwei"] = _sh.Kuwei;
                                    drsjlist["CunhuoSM"] = _sh.CunhuoSM;
                                    drsjlist["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _sh.MakeDate == null ? "" : ((DateTime)_sh.MakeDate).ToString("yyyy/MM/dd"));
                                    if (_sh.sshuliang != null)
                                        sjl_sshuliangs += (long)_sh.sshuliang;

                                    dtsjlist.Rows.Add(drsjlist);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["ShangJiaList"]));

                            DataTable dtsjlist_others = _rds.Tables["ShangJiaList_Title"];
                            DataRow drsjlist_others = dtsjlist_others.NewRow();
                            try
                            {
                                wms_rukudan rksjlist_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_sjdid) && p.IsDelete == false);
                                if (rksjlist_others != null)
                                {
                                    base_weituokehu wtkhsjlist_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == rksjlist_others.HuozhuID);
                                    if (wtkhsjlist_others != null)
                                    {
                                        drsjlist_others["HuozhuID"] = wtkhsjlist_others.Kehumingcheng;
                                    }
                                    drsjlist_others["RukudanBH"] = rksjlist_others.RukudanBH;
                                    drsjlist_others["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", rksjlist_others.RukuRQ == null ? "" : ((DateTime)rksjlist_others.RukuRQ).ToString("yyyy/MM/dd"));
                                    drsjlist_others["ChunyunYQ"] = rksjlist_others.ChunyunYQ;
                                }
                                drsjlist_others["sjlSLs"] = sjl_sshuliangs;

                                dtsjlist_others.Rows.Add(drsjlist_others);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["ShangJiaList_Title"]));
                            break;
                        case "RuKuMX":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptRuKuMX_portrait.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtRuKuMX = _rds.Tables["RuKumingxi"];
                            DataRow drRuKuMX;
                            float rkmx_DaohuoSLs = 0;
                            float rkmx_DaohuoJSs = 0;
                            var sum_rkdBH = "";
                            var sum_khDH = "";
                            List<wms_rukumx> lists = new List<wms_rukumx>();
                            foreach (string mxid in _rkmxid.Split(','))
                            {
                                if (mxid.Length > 0)
                                {
                                    //数据整合成一个list
                                    var _RuKuMXs = ServiceFactory.wms_rukumxservice.LoadSortEntities(p => p.RukuID == int.Parse(mxid) && p.IsDelete == false, true, s => s.Pihao);
                                    foreach (wms_rukumx _mx in _RuKuMXs)
                                    {
                                        lists.Add(_mx);
                                    }
                                    //入库编号总和
                                    wms_rukudan RuKuMX_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(mxid));
                                    sum_rkdBH = sum_rkdBH + RuKuMX_others.RukudanBH + ",";
                                    //客户单号总和
                                    sum_khDH = sum_khDH + RuKuMX_others.KehuDH + ",";
                                }
                            }
                            var _grouplist = from p in lists
                                             group p by new { p.ShangpinID, p.ShangpinDM, p.Pihao } into g
                                             select new
                                             {
                                                 spid = g.Key.ShangpinID,
                                                 sp = g.Key.ShangpinDM,
                                                 ph = g.Key.Pihao,
                                                 num = g.Sum(p => p.DaohuoSL)
                                             };
                            List<wms_rukumx> _newLists = new List<wms_rukumx>();
                            foreach (var gl in _grouplist)
                            {
                                foreach (var mx in lists)
                                {
                                    if (mx.ShangpinID == gl.spid && mx.Pihao == gl.ph && mx.ShangpinDM == gl.sp)
                                    {
                                        mx.DaohuoSL = gl.num;
                                        _newLists.Add(mx);
                                        break;
                                    }
                                }
                            }
                            try
                            {
                                foreach (wms_rukumx _pr in _newLists.OrderBy(p => p.Guige).ThenBy(p => p.Pihao))
                                {
                                    drRuKuMX = dtRuKuMX.NewRow();
                                    drRuKuMX["Guige"] = _pr.Guige;
                                    drRuKuMX["Pihao"] = _pr.Pihao;
                                    drRuKuMX["Zhucezheng"] = _pr.Zhucezheng;

                                    var DaohuoJS = _pr.DaohuoSL / _pr.Huansuanlv == null ? int.Parse("0") : (float)_pr.DaohuoSL / _pr.Huansuanlv;
                                    drRuKuMX["DaohuoJS"] = string.Format("{0:0.00}", DaohuoJS);
                                    rkmx_DaohuoJSs += DaohuoJS;

                                    drRuKuMX["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ == null ? "" : ((DateTime)_pr.ShixiaoRQ).ToString("yyyy/MM/dd"));

                                    rkmx_DaohuoSLs += (long)_pr.DaohuoSL;

                                    dtRuKuMX.Rows.Add(drRuKuMX);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["RuKumingxi"]));

                            DataTable dtRuKuMX_others = _rds.Tables["RuKumingxi_Title"];
                            DataRow drRuKuMX_others = dtRuKuMX_others.NewRow();
                            try
                            {
                                //入库编号总和
                                drRuKuMX_others["RukudanBH"] = sum_rkdBH;
                                //wms_rukudan:'入库单编号'&'入库日期'&'储运要求'.
                                wms_rukudan RuKuMX_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkmxid));
                                if (RuKuMX_others != null)
                                {
                                    drRuKuMX_others["RukudanBH"] = RuKuMX_others.RukudanBH;
                                    drRuKuMX_others["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", RuKuMX_others.RukuRQ == null ? "" : ((DateTime)RuKuMX_others.RukuRQ).ToString("yyyy/MM/dd"));
                                    drRuKuMX_others["ChunyunYQ"] = RuKuMX_others.ChunyunYQ;
                                    //base_weituokehu:'货主'.
                                    base_weituokehu wtkhRuKuMX_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == RuKuMX_others.HuozhuID);
                                    if (wtkhRuKuMX_others != null)
                                    {
                                        drRuKuMX_others["HuozhuID"] = wtkhRuKuMX_others.Kehumingcheng;
                                    }
                                    //base_gongyingshang:'供应商'.
                                    base_gongyingshang RuKuMXgys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == RuKuMX_others.GongyingshangID);
                                    if (RuKuMXgys != null)
                                    {
                                        drRuKuMX_others["GongyingshangID"] = RuKuMXgys.Mingcheng;
                                    }
                                    //wms_cangku:'仓库名'
                                    wms_cangku RuKuMX_ck = ServiceFactory.wms_cangkuservice.GetEntityById(p => p.ID == RuKuMX_others.CangkuID);
                                    if (RuKuMX_ck != null)
                                    {
                                        drRuKuMX_others["CangkuID"] = RuKuMX_ck.Mingcheng;
                                    }
                                }
                                //到货数量总计
                                drRuKuMX_others["rkmxdhSLs"] = string.Format("{0:0.00}", rkmx_DaohuoSLs);
                                //到货件数总计
                                drRuKuMX_others["rkmxdhJSs"] = string.Format("{0:0.00}", rkmx_DaohuoJSs);

                                dtRuKuMX_others.Rows.Add(drRuKuMX_others);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["RuKumingxi_Title"]));
                            break;
                        case "zlgl_RukuYanshouBaogao":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptZlgl_RKBG.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtzlgl_bg = _rds.Tables["zlgl_RKYSBG"];
                            DataRow drzlgl_bg;
                            long zlgl_bg_Shuliang = 0;
                            long zlgl_bg_YanshouHGSL = 0;
                            long zlgl_bg_YanshouBHGSL = 0;
                            try
                            {
                                //通过'入库单编号'查找数据
                                var _zlgl_rkyss = ServiceFactory.quan_rukuysservice.GetInrec(p => p.RukudanBH == _zlgl_rkysid && p.IsDelete == false).ToList<quan_inrec_v>();
                                foreach (quan_inrec_v _pr in _zlgl_rkyss)
                                {
                                    drzlgl_bg = dtzlgl_bg.NewRow();
                                    base_gongyingshang zlgl_gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == _pr.GongyingshangID && p.IsDelete == false);
                                    drzlgl_bg["GongyingshangID"] = zlgl_gys.Mingcheng == null ? "" : zlgl_gys.Mingcheng;
                                    drzlgl_bg["Changjia"] = _pr.Changjia == null ? "" : _pr.Changjia;
                                    drzlgl_bg["ShangpinMC"] = _pr.ShangpinMC == null ? "" : _pr.ShangpinMC;
                                    drzlgl_bg["Guige"] = _pr.Guige == null ? "" : _pr.Guige;
                                    drzlgl_bg["Pihao"] = _pr.Pihao == null ? "" : _pr.Pihao;
                                    drzlgl_bg["Xuliema"] = _pr.Xuliema == null ? "" : _pr.Xuliema;
                                    drzlgl_bg["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ == null ? "" : ((DateTime)_pr.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drzlgl_bg["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ == null ? "" : ((DateTime)_pr.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drzlgl_bg["Zhucezheng"] = _pr.Zhucezheng == null ? "" : _pr.Zhucezheng;
                                    drzlgl_bg["Shuliang"] = _pr.Shuliang == null ? int.Parse("0") : _pr.Shuliang;
                                    drzlgl_bg["ChunyunYQ"] = _pr.ChunyunYQ == null ? "" : _pr.ChunyunYQ;
                                    //验收结果
                                    drzlgl_bg["ysresult"] = MvcApplication.CheckResult[_pr.ysresult];
                                    var ysresult = MvcApplication.CheckResult[_pr.ysresult];
                                    if (ysresult == "合格")
                                    {
                                        drzlgl_bg["YanshouHGSL"] = _pr.YanshouSL == null ? int.Parse("0") : _pr.YanshouSL;
                                        drzlgl_bg["YanshouBHGSL"] = "0";
                                        zlgl_bg_YanshouHGSL += (long)_pr.YanshouSL;
                                    }
                                    if (ysresult == "不合格")
                                    {
                                        drzlgl_bg["YanshouHGSL"] = "0";
                                        drzlgl_bg["YanshouBHGSL"] = _pr.YanshouSL == null ? int.Parse("0") : _pr.YanshouSL;
                                        zlgl_bg_YanshouBHGSL += (long)_pr.YanshouSL;
                                    }
                                    drzlgl_bg["ystime"] = string.Format("{0:yyyy/MM/dd}", _pr.ystime == null ? "" : _pr.ystime.ToString("yyyy/MM/dd"));
                                    zlgl_bg_Shuliang += (long)_pr.Shuliang;

                                    dtzlgl_bg.Rows.Add(drzlgl_bg);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["zlgl_RKYSBG"]));

                            DataTable dtzlgl_bg_others = _rds.Tables["zlgl_RKYSBG_Title"];
                            DataRow drzlgl_bg_others = dtzlgl_bg_others.NewRow();
                            try
                            {
                                drzlgl_bg_others["RukudanBH"] = _zlgl_rkysid;
                                int? huozhuid_first = -1;
                                var _zlgl_rkyss = ServiceFactory.quan_rukuysservice.GetInrec(p => p.RukudanBH == _zlgl_rkysid && p.IsDelete == false).ToList<quan_inrec_v>();
                                //判断是否存在数据
                                foreach (var rkys in _zlgl_rkyss)
                                {
                                    var huozhuid = rkys.HuozhuID;
                                    if (huozhuid != null)
                                    {
                                        huozhuid_first = huozhuid;
                                        break;
                                    }
                                }
                                if (huozhuid_first > -1)
                                {
                                    base_weituokehu zlgl_wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == huozhuid_first && p.IsDelete == false);
                                    if (zlgl_wtkh != null)
                                    {
                                        drzlgl_bg_others["HuozhuID"] = zlgl_wtkh.Kehumingcheng == null ? "" : zlgl_wtkh.Kehumingcheng;
                                    }
                                }
                                foreach (var rkys in _zlgl_rkyss)
                                {
                                    if (rkys.Yanshouren != null)
                                    {
                                        drzlgl_bg_others["MakeMan"] = rkys.Yanshouren;
                                        break;
                                    }
                                }
                                //userinfo zlgl_man = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == (int)Session["user_id"] && p.IsDelete == false);
                                //if (zlgl_man != null)
                                //{
                                //    drzlgl_bg_others["MakeMan"] = zlgl_man.FullName == null ? "" : zlgl_man.FullName;
                                //}
                                wms_rukudan zlgl_rkd = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.RukudanBH == _zlgl_rkysid && p.IsDelete == false);
                                if (zlgl_rkd != null)
                                {
                                    drzlgl_bg_others["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", zlgl_rkd.RukuRQ == null ? "" : ((DateTime)zlgl_rkd.RukuRQ).ToString("yyyy/MM/dd"));

                                }
                                drzlgl_bg_others["rkysbgSLs"] = zlgl_bg_Shuliang;
                                drzlgl_bg_others["rkysbgYs"] = zlgl_bg_YanshouHGSL;
                                drzlgl_bg_others["rkysbgNs"] = zlgl_bg_YanshouBHGSL;

                                dtzlgl_bg_others.Rows.Add(drzlgl_bg_others);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["zlgl_RKYSBG_Title"]));
                            break;
                        case "zlgl_CukuFuheBaogao":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptZlgl_CKBG.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtzlgl_ckfhbg = _rds.Tables["zlgl_CKFHBG"];
                            DataRow drzlgl_ckfhbg;
                            long zlgl_ckfhbg_Shuliang = 0;
                            long zlgl_ckfhbg_FuheSL = 0;
                            long zlgl_ckfhbg_YanshouHGSL = 0;
                            long zlgl_ckfhbg_YanshouBHGSL = 0;
                            try
                            {
                                var _zlgl_ckfhs = ServiceFactory.quan_chukufhservice.GetOutrec(p => p.ChukudanBH == _zlgl_ckfhid).ToList<quan_outrec_v>();
                                foreach (quan_outrec_v _pr in _zlgl_ckfhs)
                                {
                                    drzlgl_ckfhbg = dtzlgl_ckfhbg.NewRow();
                                    drzlgl_ckfhbg["ShangpinMC"] = _pr.ShangpinMC == null ? "" : _pr.ShangpinMC;
                                    drzlgl_ckfhbg["Zhucezheng"] = _pr.Zhucezheng == null ? "" : _pr.Zhucezheng;
                                    drzlgl_ckfhbg["Guige"] = _pr.Guige == null ? "" : _pr.Guige;
                                    drzlgl_ckfhbg["Pihao"] = _pr.Pihao == null ? "" : _pr.Pihao;
                                    drzlgl_ckfhbg["Pihao1"] = _pr.Pihao1 == null ? "" : _pr.Pihao1;
                                    drzlgl_ckfhbg["Xuliema"] = _pr.Xuliema == null ? "" : _pr.Xuliema;
                                    drzlgl_ckfhbg["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ == null ? "" : ((DateTime)_pr.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drzlgl_ckfhbg["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ == null ? "" : ((DateTime)_pr.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drzlgl_ckfhbg["JianhuoSL"] = _pr.JianhuoSL == null ? int.Parse("0") : _pr.JianhuoSL;
                                    drzlgl_ckfhbg["Changjia"] = _pr.Changjia == null ? "" : _pr.Changjia;
                                    drzlgl_ckfhbg["Chandi"] = _pr.Chandi == null ? "" : _pr.Chandi;
                                    //验收结果
                                    drzlgl_ckfhbg["Fuhe"] = MvcApplication.CheckResult[_pr.Fuhe];
                                    drzlgl_ckfhbg["FuheSL"] = _pr.FuheSL == null ? int.Parse("0") : _pr.FuheSL;
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
                                    drzlgl_ckfhbg["Fuheren"] = _pr.Fuheren == null ? "" : _pr.Fuheren;
                                    drzlgl_ckfhbg["FuheSM"] = _pr.FuheSM == null ? "" : _pr.FuheSM;
                                    drzlgl_ckfhbg["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _pr.MakeDate == null ? "" : ((DateTime)_pr.MakeDate).ToString("yyyy/MM/dd"));
                                    zlgl_ckfhbg_Shuliang += (long)_pr.JianhuoSL;
                                    zlgl_ckfhbg_FuheSL += (long)_pr.FuheSL;

                                    dtzlgl_ckfhbg.Rows.Add(drzlgl_ckfhbg);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["zlgl_CKFHBG"]));

                            DataTable dtzlgl_ckfhbg_others = _rds.Tables["zlgl_CKFHBG_Title"];
                            DataRow drzlgl_ckfhbg_others = dtzlgl_ckfhbg_others.NewRow();
                            try
                            {
                                drzlgl_ckfhbg_others["ChukudanBH"] = _zlgl_ckfhid;
                                //得到第一个ID
                                var _zlgl_ckfhs = ServiceFactory.quan_chukufhservice.GetOutrec(p => p.ChukudanBH == _zlgl_ckfhid).ToList<quan_outrec_v>();
                                if (_zlgl_ckfhs != null)
                                {
                                    quan_outrec_v zlgl_first = _zlgl_ckfhs.FirstOrDefault();
                                    if (zlgl_first != null)
                                    {
                                        drzlgl_ckfhbg_others["KehuMC"] = zlgl_first.KehuMC;
                                        drzlgl_ckfhbg_others["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", zlgl_first.ChukuRQ == null ? "" : ((DateTime)zlgl_first.ChukuRQ).ToString("yyyy/MM/dd"));
                                        base_weituokehu zlgl_ckfh_wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == zlgl_first.HuozhuID);
                                        if (zlgl_ckfh_wtkh != null)
                                        {
                                            drzlgl_ckfhbg_others["HuozhuID"] = zlgl_ckfh_wtkh.Kehumingcheng;
                                        }
                                    }
                                }
                                wms_chukudan zlgl_ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ChukudanBH == _zlgl_ckfhid && p.IsDelete == false);
                                if (zlgl_ckd != null)
                                {
                                    drzlgl_ckfhbg_others["Yunsongdizhi"] = zlgl_ckd.Yunsongdizhi;
                                    drzlgl_ckfhbg_others["Beizhu"] = zlgl_ckd.Beizhu;
                                    drzlgl_ckfhbg_others["Lianxiren"] = zlgl_ckd.Lianxiren;
                                    drzlgl_ckfhbg_others["LianxiDH"] = zlgl_ckd.LianxiDH;
                                }
                                //统计
                                drzlgl_ckfhbg_others["ckfhbgSLs"] = zlgl_ckfhbg_Shuliang;
                                drzlgl_ckfhbg_others["ckfhbgYs"] = zlgl_ckfhbg_YanshouHGSL;
                                drzlgl_ckfhbg_others["ckfhbgNs"] = zlgl_ckfhbg_YanshouBHGSL;
                                drzlgl_ckfhbg_others["ckfhbgFHSLs"] = zlgl_ckfhbg_FuheSL;

                                dtzlgl_ckfhbg_others.Rows.Add(drzlgl_ckfhbg_others);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["zlgl_CKFHBG_Title"]));
                            break;
                        case "shangpinxx_shouying":
                            int userid = (int)Session["user_id"];
                            string pagetag = "base_shangpinxx_index";
                            Expression<Func<base_shangpin_v, bool>> where = PredicateExtensionses.True<base_shangpin_v>();
                            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
                            if (sc != null)
                            {
                                string[] sclist = sc.ConditionInfo.Split(';');
                                foreach (string scl in sclist)
                                {
                                    string[] scld = scl.Split(',');
                                    switch (scld[0])
                                    {
                                        case "huozhuid":
                                            string huozhuid = scld[1];
                                            string huozhuidequal = scld[2];
                                            string huozhuidand = scld[3];
                                            if (!string.IsNullOrEmpty(huozhuid))
                                            {
                                                if (huozhuidequal.Equals("="))
                                                {
                                                    if (huozhuidand.Equals("and"))
                                                        where = where.And(base_shangpin_v => base_shangpin_v.huozhuid == int.Parse(huozhuid));
                                                    else
                                                        where = where.Or(base_shangpin_v => base_shangpin_v.huozhuid == int.Parse(huozhuid));
                                                }
                                            }
                                            break;
                                        case "daima":
                                            string daima = scld[1];
                                            string daimaequal = scld[2];
                                            string daimaand = scld[3];
                                            if (!string.IsNullOrEmpty(daima))
                                            {
                                                if (daimaequal.Equals("="))
                                                {
                                                    if (daimaand.Equals("and"))
                                                        where = where.And(base_shangpin_v => base_shangpin_v.daima == daima);
                                                    else
                                                        where = where.Or(base_shangpin_v => base_shangpin_v.daima == daima);
                                                }
                                                if (daimaequal.Equals("like"))
                                                {
                                                    if (daimaand.Equals("and"))
                                                        where = where.And(base_shangpin_v => base_shangpin_v.daima.Contains(daima));
                                                    else
                                                        where = where.Or(base_shangpin_v => base_shangpin_v.daima.Contains(daima));
                                                }
                                            }
                                            break;
                                        case "mingcheng":
                                            string mingcheng = scld[1];
                                            string mingchengequal = scld[2];
                                            string mingchengand = scld[3];
                                            if (!string.IsNullOrEmpty(mingcheng))
                                            {
                                                if (mingchengequal.Equals("="))
                                                {
                                                    if (mingchengand.Equals("and"))
                                                        where = where.And(base_shangpin_v => base_shangpin_v.mingcheng == mingcheng);
                                                    else
                                                        where = where.Or(base_shangpin_v => base_shangpin_v.mingcheng == mingcheng);
                                                }
                                                if (mingchengequal.Equals("like"))
                                                {
                                                    if (mingchengand.Equals("and"))
                                                        where = where.And(base_shangpin_v => base_shangpin_v.mingcheng.Contains(mingcheng));
                                                    else
                                                        where = where.Or(base_shangpin_v => base_shangpin_v.mingcheng.Contains(mingcheng));
                                                }
                                            }
                                            break;
                                        case "zhucezhengbh":
                                            string zhucezhengbh = scld[1];
                                            string zhucezhengbhequal = scld[2];
                                            string zhucezhengbhand = scld[3];
                                            if (!string.IsNullOrEmpty(zhucezhengbh))
                                            {
                                                if (zhucezhengbhequal.Equals("="))
                                                {
                                                    if (zhucezhengbhand.Equals("and"))
                                                        where = where.And(p => p.ZhucezhengBH == zhucezhengbh);
                                                    else
                                                        where = where.Or(p => p.ZhucezhengBH == zhucezhengbh);
                                                }
                                                if (zhucezhengbhequal.Equals("like"))
                                                {
                                                    if (zhucezhengbhand.Equals("and"))
                                                        where = where.And(p => p.ZhucezhengBH.Contains(zhucezhengbh));
                                                    else
                                                        where = where.Or(p => p.ZhucezhengBH.Contains(zhucezhengbh));
                                                }
                                            }
                                            break;
                                        case "shouying":
                                            string shouying = scld[1];
                                            string shouyingequal = scld[2];
                                            string shouyingand = scld[3];
                                            if (shouying == "0")
                                            {
                                                shouying = "";
                                            }
                                            if (!string.IsNullOrEmpty(shouying))
                                            {
                                                if (shouyingequal.Equals("="))
                                                {
                                                    if (shouyingand.Equals("and"))
                                                        where = where.And(p => p.Shouying == int.Parse(shouying));
                                                    else
                                                        where = where.Or(p => p.Shouying == int.Parse(shouying));
                                                }
                                            }
                                            break;
                                        case "shenchasf":
                                            string shenchasf = scld[1];
                                            string shenchasfequal = scld[2];
                                            string shenchasfand = scld[3];
                                            if (shenchasf == "1")
                                            {
                                                shenchasf = "true";
                                            }
                                            else if (shenchasf == "2")
                                            {
                                                shenchasf = "false";
                                            }
                                            else if (shenchasf == "-1")
                                            {
                                                shenchasf = "";
                                            }
                                            if (!string.IsNullOrEmpty(shenchasf))
                                            {
                                                if (shenchasfequal.Equals("="))
                                                {
                                                    if (shenchasfand.Equals("and"))
                                                        where = where.And(p => p.ShenchaSF == bool.Parse(shenchasf));
                                                    else
                                                        where = where.Or(p => p.ShenchaSF == bool.Parse(shenchasf));
                                                }
                                            }
                                            break;
                                        case "gongyingid":
                                            string gongyingid = scld[1];
                                            string gongyingidequal = scld[2];
                                            string gongyingidand = scld[3];
                                            if (!string.IsNullOrEmpty(gongyingid))
                                            {
                                                if (gongyingidequal.Equals("="))
                                                {
                                                    if (gongyingidand.Equals("and"))
                                                        where = where.And(p => p.GongyingID == int.Parse(gongyingid));
                                                    else
                                                        where = where.Or(p => p.GongyingID == int.Parse(gongyingid));
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            where = where.And(base_shangpin_v => base_shangpin_v.IsDelete == false);

                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptSpxx_shouying.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtspxx = _rds.Tables["Spxx_shouying"];
                            DataRow drspxx;
                            try
                            {
                                var _spxxs = ServiceFactory.base_shangpinxxservice.LoadShangpinAll(where.Compile()).ToList<base_shangpin_v>();
                                foreach (base_shangpin_v _pr in _spxxs)
                                {
                                    drspxx = dtspxx.NewRow();
                                    //base_shangpinxx
                                    drspxx["ZhucezhengBH"] = _pr.ZhucezhengBH;
                                    drspxx["Daima"] = _pr.daima;
                                    drspxx["Guige"] = _pr.Guige;
                                    drspxx["Chandi"] = _pr.chandi;
                                    drspxx["Mingcheng"] = _pr.mingcheng;
                                    //base_shangpinzcz
                                    var _spzczs = ServiceFactory.base_shangpinzczservice.GetEntityById(p => p.ID == _pr.zhucezhengid && p.IsDelete == false);
                                    if (_spzczs != null)
                                    {
                                        drspxx["ZhucezhengYXQ"] = string.Format("{0:yyyy/MM/dd}", _spzczs.ZhucezhengYXQ == null ? "" : ((DateTime)_spzczs.ZhucezhengYXQ).ToString("yyyy/MM/dd"));
                                    }
                                    //quan_shouyingsq
                                    var _sysqs = ServiceFactory.quan_shouyingsqservice.GetEntityById(p => p.ShenqingID == _pr.id && p.IsDelete == false && p.Zhuangtai == 5);
                                    if (_sysqs != null)
                                    {
                                        drspxx["Shenheshuoming"] = _sysqs.Shenheshuoming;
                                        drspxx["Shenheren"] = _sysqs.Shenheren;
                                        drspxx["FuzerenSM"] = _sysqs.FuzerenSM;
                                        drspxx["Fuzeren"] = _sysqs.Fuzeren;
                                        drspxx["SQshijian"] = string.Format("{0:yyyy/MM/dd}", _sysqs.SQshijian == null ? "" : ((DateTime)_sysqs.SQshijian).ToString("yyyy/MM/dd"));
                                    }
                                    //base_huozhushouquan
                                    var _hzsqs = ServiceFactory.base_huozhushouquanservice.GetEntityById(p => p.HuozhuID == _pr.huozhuid && p.ShouquanID == _pr.GongyingID && p.IsDelete == false);
                                    if (_hzsqs != null)
                                    {
                                        drspxx["ShouquanshuYXQ"] = string.Format("{0:yyyy/MM/dd}", _hzsqs.ShouquanshuYXQ == null ? "" : ((DateTime)_hzsqs.ShouquanshuYXQ).ToString("yyyy/MM/dd"));
                                    }

                                    dtspxx.Rows.Add(drspxx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["Spxx_shouying"]));
                            //页眉&页脚
                            DataTable dtspxx_others = _rds.Tables["Spxx_shouying_Title"];
                            DataRow drspxx_others = dtspxx_others.NewRow();
                            try
                            {
                                int? first_huozhuid = -1;
                                var _spxxs = ServiceFactory.base_shangpinxxservice.LoadShangpinAll(where.Compile()).ToList<base_shangpin_v>();
                                foreach (base_shangpin_v _pr in _spxxs)
                                {
                                    if (_pr.huozhuid != null)
                                    {
                                        first_huozhuid = _pr.huozhuid;
                                        break;
                                    }
                                }
                                //base_gongyingshang
                                if (first_huozhuid >= 0)
                                {
                                    var spxx_wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == first_huozhuid && p.IsDelete == false);
                                    if (spxx_wtkh != null)
                                    {
                                        drspxx_others["Kehumingcheng"] = spxx_wtkh.Kehumingcheng;
                                        drspxx_others["YingyezhizhaoBH"] = spxx_wtkh.YingyezhizhaoBH;
                                        drspxx_others["YingyezhizhaoYXQ"] = string.Format("{0:yyyy/MM/dd}", spxx_wtkh.YingyezhizhaoYXQ == null ? "" : ((DateTime)spxx_wtkh.YingyezhizhaoYXQ).ToString("yyyy/MM/dd"));
                                        drspxx_others["JingyingxukeBH"] = spxx_wtkh.JingyingxukeBH;
                                        drspxx_others["JingyingxukeYXQ"] = string.Format("{0:yyyy/MM/dd}", spxx_wtkh.JingyingxukeYXQ == null ? "" : ((DateTime)spxx_wtkh.JingyingxukeYXQ).ToString("yyyy/MM/dd"));
                                        drspxx_others["Lianxiren"] = spxx_wtkh.Lianxiren;
                                        drspxx_others["Lianxidianhua"] = spxx_wtkh.Lianxidianhua;
                                        drspxx_others["BeianBH"] = spxx_wtkh.BeianBH;
                                        drspxx_others["BeianYXQ"] = string.Format("{0:yyyy/MM/dd}", spxx_wtkh.BeianYXQ == null ? "" : ((DateTime)spxx_wtkh.BeianYXQ).ToString("yyyy/MM/dd"));
                                        dtspxx_others.Rows.Add(drspxx_others);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["Spxx_shouying_Title"]));
                            break;
                        case "YiweiInfo":
                            int yw_userid = (int)Session["user_id"];
                            string yw_pagetag = "wms_yiwei_index";
                            Expression<Func<wms_yiwei, bool>> yw_where = PredicateExtensionses.True<wms_yiwei>();
                            searchcondition yw_sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == yw_userid && searchcondition.PageBrief == yw_pagetag);
                            if (yw_sc != null && yw_sc.ConditionInfo != null)
                            {
                                string[] sclist = yw_sc.ConditionInfo.Split(';');
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
                                                        yw_where = yw_where.And(p => p.KWBH == kwbh);
                                                    else
                                                        yw_where = yw_where.Or(p => p.KWBH == kwbh);
                                                }
                                                if (kwbhequal.Equals("like"))
                                                {
                                                    if (kwbhand.Equals("and"))
                                                        yw_where = yw_where.And(p => p.KWBH.Contains(kwbh));
                                                    else
                                                        yw_where = yw_where.Or(p => p.KWBH.Contains(kwbh));
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
                                                        yw_where = yw_where.And(p => p.XKWBH == xkwbh);
                                                    else
                                                        yw_where = yw_where.Or(p => p.XKWBH == xkwbh);
                                                }
                                                if (xkwbhequal.Equals("like"))
                                                {
                                                    if (xkwbhand.Equals("and"))
                                                        yw_where = yw_where.And(p => p.XKWBH.Contains(xkwbh));
                                                    else
                                                        yw_where = yw_where.Or(p => p.XKWBH.Contains(xkwbh));
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            yw_where = yw_where.And(wms_yiwei => wms_yiwei.IsDelete == false);

                            var rptYiweiInfos = ServiceFactory.wms_yiweiservice.LoadSortEntities(yw_where.Compile(), false, wms_yiwei => wms_yiwei.ID);
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
                        case "jianhuo_CKFuhejianyan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptJH_CKFuhejianyan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtjh_ckfhjy = _rds.Tables["JH_CKfuhejianyan"];
                            DataRow drjh_ckfhjy;
                            //long JH_CKFHJY_SLs = 0;
                            float ckfh_jianshu = 0;
                            float JH_CKFHJY_JSs = 0;
                            try
                            {
                                var _jhds = ob_wms_jianhuoservice.GetPickDetail(int.Parse(_JH_ckjyid), p => p.DaijianSL > 0).OrderBy(p => p.Kuwei).ThenBy(p => p.Pihao).ToList<wms_pick_v>();
                                foreach (wms_pick_v _pv in _jhds)
                                {
                                    drjh_ckfhjy = dtjh_ckfhjy.NewRow();
                                    drjh_ckfhjy["Mingcheng"] = _pv.ShangpinMC;
                                    drjh_ckfhjy["Zhucezheng"] = _pv.Zhucezheng;
                                    drjh_ckfhjy["Guige"] = _pv.Guige;
                                    drjh_ckfhjy["Pihao"] = _pv.Pihao;
                                    drjh_ckfhjy["Pihao1"] = _pv.Pihao1;
                                    drjh_ckfhjy["Xuliema"] = _pv.Xuliema;
                                    drjh_ckfhjy["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pv.ShengchanRQ == null ? "" : ((DateTime)_pv.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drjh_ckfhjy["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pv.ShixiaoRQ == null ? "" : ((DateTime)_pv.ShixiaoRQ).ToString("yyyy/MM/dd"));

                                    ckfh_jianshu = _pv.DaijianSL / _pv.Huansuanlv == null ? int.Parse("0") : (float)_pv.DaijianSL / _pv.Huansuanlv;
                                    JH_CKFHJY_JSs += ckfh_jianshu;
                                    drjh_ckfhjy["JianShu"] = ckfh_jianshu;

                                    //JH_CKFHJY_SLs += (long)_pv.DaijianSL;
                                    drjh_ckfhjy["Changjia"] = _pv.Changjia;
                                    drjh_ckfhjy["Chandi"] = _pv.Chandi;

                                    dtjh_ckfhjy.Rows.Add(drjh_ckfhjy);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["JH_CKfuhejianyan"]));

                            DataTable dtjh_ckfhjy_t = _rds.Tables["JH_CKfuhejianyan_Title"];
                            DataRow drjhdt_t = dtjh_ckfhjy_t.NewRow();
                            try
                            {
                                wms_chukudan tempdata = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_JH_ckjyid) && p.IsDelete == false);
                                if (tempdata != null)
                                {
                                    drjhdt_t["Yunsongdizhi"] = tempdata.Yunsongdizhi;
                                    drjhdt_t["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", tempdata.ChukuRQ == null ? "" : ((DateTime)tempdata.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drjhdt_t["ChukudanBH"] = tempdata.ChukudanBH;
                                    drjhdt_t["Lianxiren"] = tempdata.Lianxiren;
                                    drjhdt_t["LianxiDH"] = tempdata.LianxiDH;
                                    drjhdt_t["Beizhu"] = tempdata.Beizhu;
                                    drjhdt_t["KehuMC"] = tempdata.KehuMC;
                                    //drjhdt_t["JH_CKFHJY_SLs"] = JH_CKFHJY_SLs;
                                    drjhdt_t["JH_CKFHJY_JSs"] = JH_CKFHJY_JSs;

                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == tempdata.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drjhdt_t["HuozhuID"] = wtkhdata.Kehumingcheng;
                                    }
                                    dtjh_ckfhjy_t.Rows.Add(drjhdt_t);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["JH_CKfuhejianyan_Title"]));
                            break;
                        case "inReport":
                            int ir_userid = (int)Session["user_id"];
                            string ir_pagetag = "wms_inreport_list";
                            Expression<Func<wms_recievelist_v, bool>> ir_where = PredicateExtensionses.True<wms_recievelist_v>();
                            searchcondition ir_sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == ir_userid && searchcondition.PageBrief == ir_pagetag);
                            if (ir_sc != null && ir_sc.ConditionInfo != null)
                            {
                                string[] sclist = ir_sc.ConditionInfo.Split(';');
                                foreach (string scl in sclist)
                                {
                                    string[] scld = scl.Split(',');
                                    switch (scld[0])
                                    {
                                        case "rukubh":
                                            string rukubh = scld[1];
                                            string rukubhequal = scld[2];
                                            string rukubhand = scld[3];
                                            if (!string.IsNullOrEmpty(rukubh))
                                            {
                                                if (rukubhequal.Equals("="))
                                                {
                                                    if (rukubhand.Equals("and"))
                                                        ir_where = ir_where.And(p => p.RukudanBH == rukubh);
                                                    else
                                                        ir_where = ir_where.Or(p => p.RukudanBH == rukubh);
                                                }
                                                if (rukubhequal.Equals("like"))
                                                {
                                                    if (rukubhand.Equals("and"))
                                                        ir_where = ir_where.And(p => p.RukudanBH.Contains(rukubh));
                                                    else
                                                        ir_where = ir_where.Or(p => p.RukudanBH.Contains(rukubh));
                                                }
                                            }
                                            break;
                                        case "ShangpinMC":
                                            string ShangpinMC = scld[1];
                                            string ShangpinMCequal = scld[2];
                                            string ShangpinMCand = scld[3];
                                            if (!string.IsNullOrEmpty(ShangpinMC))
                                            {
                                                if (ShangpinMCequal.Equals("="))
                                                {
                                                    if (ShangpinMCand.Equals("and"))
                                                        ir_where = ir_where.And(p => p.ShangpinMC == ShangpinMC);
                                                    else
                                                        ir_where = ir_where.Or(p => p.ShangpinMC == ShangpinMC);
                                                }
                                                if (ShangpinMCequal.Equals("like"))
                                                {
                                                    if (ShangpinMCand.Equals("and"))
                                                        ir_where = ir_where.And(p => p.ShangpinMC.Contains(ShangpinMC));
                                                    else
                                                        ir_where = ir_where.Or(p => p.ShangpinMC.Contains(ShangpinMC));
                                                }
                                            }
                                            break;
                                        case "Guige":
                                            string Guige = scld[1];
                                            string Guigeequal = scld[2];
                                            string Guigeand = scld[3];
                                            if (!string.IsNullOrEmpty(Guige))
                                            {
                                                if (Guigeequal.Equals("="))
                                                {
                                                    if (Guigeand.Equals("and"))
                                                        ir_where = ir_where.And(p => p.Guige == Guige);
                                                    else
                                                        ir_where = ir_where.Or(p => p.Guige == Guige);
                                                }
                                                if (Guigeequal.Equals("like"))
                                                {
                                                    if (Guigeand.Equals("and"))
                                                        ir_where = ir_where.And(p => p.Guige.Contains(Guige));
                                                    else
                                                        ir_where = ir_where.Or(p => p.Guige.Contains(Guige));
                                                }
                                            }
                                            break;
                                        case "Pihao":
                                            string Pihao = scld[1];
                                            string Pihaoequal = scld[2];
                                            string Pihaoand = scld[3];
                                            if (!string.IsNullOrEmpty(Pihao))
                                            {
                                                if (Pihaoequal.Equals("="))
                                                {
                                                    if (Pihaoand.Equals("and"))
                                                        ir_where = ir_where.And(p => p.Pihao == Pihao);
                                                    else
                                                        ir_where = ir_where.Or(p => p.Pihao == Pihao);
                                                }
                                                if (Pihaoequal.Equals("like"))
                                                {
                                                    if (Pihaoand.Equals("and"))
                                                        ir_where = ir_where.And(p => p.Pihao.Contains(Pihao));
                                                    else
                                                        ir_where = ir_where.Or(p => p.Pihao.Contains(Pihao));
                                                }
                                            }
                                            break;
                                        case "RukuRQ":
                                            string RukuRQ = scld[1];
                                            string RukuRQequal = scld[2];
                                            string RukuRQand = scld[3];
                                            if (!string.IsNullOrEmpty(RukuRQ))
                                            {
                                                if (RukuRQequal.Equals("="))
                                                {
                                                    if (RukuRQand.Equals("and"))
                                                        ir_where = ir_where.And(p => p.RukuRQ == DateTime.Parse(RukuRQ));
                                                    else
                                                        ir_where = ir_where.Or(p => p.RukuRQ == DateTime.Parse(RukuRQ));
                                                }
                                                if (RukuRQequal.Equals(">"))
                                                {
                                                    if (RukuRQand.Equals("and"))
                                                        ir_where = ir_where.And(p => p.RukuRQ > DateTime.Parse(RukuRQ));
                                                    else
                                                        ir_where = ir_where.Or(p => p.RukuRQ > DateTime.Parse(RukuRQ));
                                                }
                                                if (RukuRQequal.Equals("<"))
                                                {
                                                    if (RukuRQand.Equals("and"))
                                                        ir_where = ir_where.And(p => p.RukuRQ < DateTime.Parse(RukuRQ));
                                                    else
                                                        ir_where = ir_where.Or(p => p.RukuRQ < DateTime.Parse(RukuRQ));
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptInReport.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtir = _rds.Tables["table_InReport"];
                            var ir_rkd = ServiceFactory.wms_rukudanservice.GetInList(ir_where.Compile());
                            DataRow drir;
                            foreach (wms_recievelist_v _pr in ir_rkd)
                            {
                                drir = dtir.NewRow();
                                var ir_wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == _pr.HuozhuID && p.IsDelete == false);
                                if (ir_wtkh != null)
                                {
                                    drir["Kehumingcheng"] = ir_wtkh.Kehumingcheng == null ? "" : ir_wtkh.Kehumingcheng;
                                }
                                drir["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.RukuRQ == null ? "" : ((DateTime)_pr.RukuRQ).ToString("yyyy/MM/dd"));
                                drir["YewuLX"] = MvcApplication.EntryType[_pr.YewuLX] == null ? MvcApplication.EntryType[0] : MvcApplication.EntryType[_pr.YewuLX];
                                drir["ShangpinMC"] = _pr.ShangpinMC == null ? "" : _pr.ShangpinMC;
                                drir["Guige"] = _pr.Guige == null ? "" : _pr.Guige;
                                drir["Changjia"] = _pr.Changjia == null ? "" : _pr.Changjia;
                                drir["Zhucezheng"] = _pr.Zhucezheng == null ? "" : _pr.Zhucezheng;
                                drir["Pihao"] = _pr.Pihao == null ? "" : _pr.Pihao;
                                drir["Xuliema"] = _pr.Xuliema == null ? "" : _pr.Xuliema;
                                drir["MAN_EXP"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ == null ? "" : ((DateTime)_pr.ShengchanRQ).ToString("yyyy/MM/dd")) + "/" + string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ == null ? "" : ((DateTime)_pr.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                drir["ChushiSL"] = _pr.ChushiSL == null ? float.Parse("0") : _pr.ChushiSL;
                                drir["JibenDW"] = _pr.JibenDW == null ? "" : _pr.JibenDW;
                                drir["Kuwei"] = _pr.Kuwei == null ? "" : _pr.Kuwei;
                                drir["HegeSF"] = string.Format("{0}", _pr.HegeSF == true ? "合格" : "不合格");
                                drir["CunhuoSM"] = _pr.CunhuoSM == null ? "" : _pr.CunhuoSM;
                                dtir.Rows.Add(drir);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["table_InReport"]));
                            break;
                        case "outReport":
                            int or_userid = (int)Session["user_id"];
                            string or_pagetag = "wms_outreport_list";
                            Expression<Func<wms_outdetaillist_v, bool>> or_where = PredicateExtensionses.True<wms_outdetaillist_v>();
                            searchcondition or_sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == or_userid && searchcondition.PageBrief == or_pagetag);
                            if (or_sc != null && or_sc.ConditionInfo != null)
                            {
                                string[] sclist = or_sc.ConditionInfo.Split(';');
                                foreach (string scl in sclist)
                                {
                                    string[] scld = scl.Split(',');
                                    switch (scld[0])
                                    {
                                        case "chukubh":
                                            string chukubh = scld[1];
                                            string chukubhequal = scld[2];
                                            string chukubhand = scld[3];
                                            if (!string.IsNullOrEmpty(chukubh))
                                            {
                                                if (chukubhequal.Equals("="))
                                                {
                                                    if (chukubhand.Equals("and"))
                                                        or_where = or_where.And(p => p.ChukudanBH == chukubh);
                                                    else
                                                        or_where = or_where.Or(p => p.ChukudanBH == chukubh);
                                                }
                                                if (chukubhequal.Equals("like"))
                                                {
                                                    if (chukubhand.Equals("and"))
                                                        or_where = or_where.And(p => p.ChukudanBH.Contains(chukubh));
                                                    else
                                                        or_where = or_where.Or(p => p.ChukudanBH.Contains(chukubh));
                                                }
                                            }
                                            break;
                                        case "ShangpinMC":
                                            string ShangpinMC = scld[1];
                                            string ShangpinMCequal = scld[2];
                                            string ShangpinMCand = scld[3];
                                            if (!string.IsNullOrEmpty(ShangpinMC))
                                            {
                                                if (ShangpinMCequal.Equals("="))
                                                {
                                                    if (ShangpinMCand.Equals("and"))
                                                        or_where = or_where.And(p => p.ShangpinMC == ShangpinMC);
                                                    else
                                                        or_where = or_where.Or(p => p.ShangpinMC == ShangpinMC);
                                                }
                                                if (ShangpinMCequal.Equals("like"))
                                                {
                                                    if (ShangpinMCand.Equals("and"))
                                                        or_where = or_where.And(p => p.ShangpinMC.Contains(ShangpinMC));
                                                    else
                                                        or_where = or_where.Or(p => p.ShangpinMC.Contains(ShangpinMC));
                                                }
                                            }
                                            break;
                                        case "Guige":
                                            string Guige = scld[1];
                                            string Guigeequal = scld[2];
                                            string Guigeand = scld[3];
                                            if (!string.IsNullOrEmpty(Guige))
                                            {
                                                if (Guigeequal.Equals("="))
                                                {
                                                    if (Guigeand.Equals("and"))
                                                        or_where = or_where.And(p => p.Guige == Guige);
                                                    else
                                                        or_where = or_where.Or(p => p.Guige == Guige);
                                                }
                                                if (Guigeequal.Equals("like"))
                                                {
                                                    if (Guigeand.Equals("and"))
                                                        or_where = or_where.And(p => p.Guige.Contains(Guige));
                                                    else
                                                        or_where = or_where.Or(p => p.Guige.Contains(Guige));
                                                }
                                            }
                                            break;
                                        case "Pihao":
                                            string Pihao = scld[1];
                                            string Pihaoequal = scld[2];
                                            string Pihaoand = scld[3];
                                            if (!string.IsNullOrEmpty(Pihao))
                                            {
                                                if (Pihaoequal.Equals("="))
                                                {
                                                    if (Pihaoand.Equals("and"))
                                                        or_where = or_where.And(p => p.Pihao == Pihao);
                                                    else
                                                        or_where = or_where.Or(p => p.Pihao == Pihao);
                                                }
                                                if (Pihaoequal.Equals("like"))
                                                {
                                                    if (Pihaoand.Equals("and"))
                                                        or_where = or_where.And(p => p.Pihao.Contains(Pihao));
                                                    else
                                                        or_where = or_where.Or(p => p.Pihao.Contains(Pihao));
                                                }
                                            }
                                            break;
                                        case "ChukuRQ":
                                            string ChukuRQ = scld[1];
                                            string ChukuRQequal = scld[2];
                                            string ChukuRQand = scld[3];
                                            if (!string.IsNullOrEmpty(ChukuRQ))
                                            {
                                                if (ChukuRQequal.Equals("="))
                                                {
                                                    if (ChukuRQand.Equals("and"))
                                                        or_where = or_where.And(p => p.ChukuRQ == DateTime.Parse(ChukuRQ));
                                                    else
                                                        or_where = or_where.Or(p => p.ChukuRQ == DateTime.Parse(ChukuRQ));
                                                }
                                                if (ChukuRQequal.Equals(">"))
                                                {
                                                    if (ChukuRQand.Equals("and"))
                                                        or_where = or_where.And(p => p.ChukuRQ > DateTime.Parse(ChukuRQ));
                                                    else
                                                        or_where = or_where.Or(p => p.ChukuRQ > DateTime.Parse(ChukuRQ));
                                                }
                                                if (ChukuRQequal.Equals("<"))
                                                {
                                                    if (ChukuRQand.Equals("and"))
                                                        or_where = or_where.And(p => p.ChukuRQ < DateTime.Parse(ChukuRQ));
                                                    else
                                                        or_where = or_where.Or(p => p.ChukuRQ < DateTime.Parse(ChukuRQ));
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptOutReport.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtor = _rds.Tables["table_outReport"];
                            var or_ckd = ServiceFactory.wms_chukudanservice.GetOutList(or_where.Compile());
                            DataRow dror;
                            foreach (wms_outdetaillist_v _pr in or_ckd)
                            {
                                dror = dtor.NewRow();
                                var or_wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == _pr.HuozhuID && p.IsDelete == false);
                                if (or_wtkh != null)
                                {
                                    dror["Kehumingcheng"] = or_wtkh.Kehumingcheng == null ? "" : or_wtkh.Kehumingcheng;
                                }
                                dror["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ChukuRQ == null ? "" : ((DateTime)_pr.ChukuRQ).ToString("yyyy/MM/dd"));
                                dror["YewuLX"] = MvcApplication.OutgoingType[_pr.YewuLX] == null ? MvcApplication.OutgoingType[0] : MvcApplication.OutgoingType[_pr.YewuLX];
                                dror["ShangpinMC"] = _pr.ShangpinMC == null ? "" : _pr.ShangpinMC;
                                dror["Guige"] = _pr.Guige == null ? "" : _pr.Guige;
                                dror["Changjia"] = _pr.Changjia == null ? "" : _pr.Changjia;
                                dror["Zhucezheng"] = _pr.Zhucezheng == null ? "" : _pr.Zhucezheng;
                                dror["Pihao"] = _pr.Pihao == null ? "" : _pr.Pihao;
                                dror["Xuliema"] = _pr.Xuliema == null ? "" : _pr.Xuliema;
                                dror["ChunyunYQ"] = _pr.ChunyunYQ == null ? "" : _pr.ChunyunYQ;
                                dror["JibenDW"] = _pr.JibenDW == null ? "" : _pr.JibenDW;
                                dror["ChukuSL"] = _pr.ChukuSL == null ? float.Parse("0") : _pr.ChukuSL;
                                dror["KehuMC"] = _pr.KehuMC == null ? "" : _pr.KehuMC;
                                dror["Yunsongdizhi"] = _pr.Yunsongdizhi == null ? "" : _pr.Yunsongdizhi;
                                dror["Lianxiren"] = _pr.Lianxiren == null ? "" : _pr.Lianxiren;
                                dror["LianxiDH"] = _pr.LianxiDH == null ? "" : _pr.LianxiDH;
                                dror["Beizhu"] = _pr.Beizhu == null ? "" : _pr.Beizhu;
                                dtor.Rows.Add(dror);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["table_outReport"]));
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }
}