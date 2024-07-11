import 'package:flutter/material.dart';
import 'dart:convert' as convert;
import 'package:http/http.dart' as http;
import 'package:flutter_application_1/laporan.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';

class LaporanPenjualanAll {
  final String tahun;
  final int keuntungan;
  final int pendapatan;
    final int totalTransaksi;


  LaporanPenjualanAll({
    required this.tahun,
    required this.keuntungan,
    required this.pendapatan,
    required this.totalTransaksi,
  });

  factory LaporanPenjualanAll.fromJson(Map<String, dynamic> json) {
    return LaporanPenjualanAll(
      tahun: json['tahun'],
      keuntungan: int.parse(json['keuntungan']),
      pendapatan: int.parse(json['pendapatan']),
      totalTransaksi: int.parse(json['total_transaksi']),
    );
  }
}

class ApiService {
  final String baseUrl = 'https://praktikum-cpanel-unbin.com/golang_api/delimart_api/';

  Future<List<LaporanPenjualanAll>?> fetchFromServer() async {
    var url = "$baseUrl/laporan_penjualan_all";
    var response = await http.get(Uri.parse(url));

    if (response.statusCode == 200) {
      var body = convert.jsonDecode(response.body);
      if (body == null || body.isEmpty) {
        return null;
      }
      List<LaporanPenjualanAll> LaporanPenjualanAllList = [];
      for (final item in body) {
        LaporanPenjualanAllList.add(LaporanPenjualanAll.fromJson(item));
      }
      return LaporanPenjualanAllList;
    } else {
      throw Exception('Failed to load LaporanPenjualanAll');
    }
  }
}

// Main Application
void main() {
  runApp(LaporanPenjualanAllPage());
}

class LaporanPenjualanAllPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Delimart App',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: LaporanPenjualanAllList(),
    );
  }
}

class LaporanPenjualanAllList extends StatefulWidget {
  @override
  _LaporanPenjualanAllListState createState() => _LaporanPenjualanAllListState();
}

class _LaporanPenjualanAllListState extends State<LaporanPenjualanAllList> {
  late Future<List<LaporanPenjualanAll>?> futureLaporanPenjualanAll;

  @override
  void initState() {
    super.initState();
    futureLaporanPenjualanAll = ApiService().fetchFromServer();
  }
Future<void> _laporan() async {
    setState(() {
      futureLaporanPenjualanAll = ApiService().fetchFromServer();
    });
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: Text(
          "Laporan Penjualan Selama Ini" ,
          style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold,fontSize: 15.0,),
        ),
        
       
        iconTheme: const IconThemeData(
          color: Colors.white, // Change your color here
        ),
        leading: IconButton(
          icon: Icon(Icons.arrow_back),
          onPressed: () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanPage()),
            );
          },
        ),
      ),
      body: LiquidPullToRefresh(
        onRefresh: _laporan,
        color:  Color.fromARGB(255, 2, 10, 74),
        height: 200,
        backgroundColor:  Colors.white,
        animSpeedFactor: 1,
        showChildOpacityTransition: false,
        child:FutureBuilder<List<LaporanPenjualanAll>?>(
        future: futureLaporanPenjualanAll,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return Center(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Text('Laporan Penjualan Selama ini kosong'),
                    SizedBox(height: 20),
                    ElevatedButton(
                      onPressed: _laporan,
                      child: Text('Refresh'),
                    ),
                  ],
                ),
              );
          } else {
            return ListView.builder(
              itemCount: snapshot.data!.length,
              itemBuilder: (context, index) {
                final LaporanPenjualanAll laporan = snapshot.data![index];
                return Card(
                  shape: RoundedRectangleBorder(
                    side: const BorderSide(
                      color: Color.fromARGB(255, 2, 10, 74),
                      width: 2,
                    ),
                    borderRadius: BorderRadius.circular(10),
                  ),
                  child: ListTile(
                    title: Text(
                      laporan.tahun,
                      style: TextStyle(fontWeight: FontWeight.bold),
                    ),
                    subtitle: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text('Total Transaksi: ${laporan.totalTransaksi}'),
                        Text('Pendapatan: ${laporan.pendapatan}'),
                        Text('Keuntungan: ${laporan.keuntungan}'),
                      ],
                    ),
                  ),
                );
              },
            );
          }
        },
      ),
      )
    );
  }
}
