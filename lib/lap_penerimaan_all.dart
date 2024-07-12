import 'package:flutter/material.dart';
import 'dart:convert' as convert;
import 'package:http/http.dart' as http;
import 'package:flutter_application_1/laporan.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';


class LaporanPenerimaanAll {
  final String tahun;
  final int pengeluaran;
  final int transaksi;

  LaporanPenerimaanAll({
    required this.tahun,
    required this.pengeluaran,
    required this.transaksi,
  });

  factory LaporanPenerimaanAll.fromJson(Map<String, dynamic> json) {
    return LaporanPenerimaanAll(
      tahun: json['Tahunan'],
      pengeluaran: int.parse(json['pengeluaran']),
      transaksi: int.parse(json['total_transaksi']),
    );
  }
}

class ApiService {
  final String baseUrl = 'https://praktikum-cpanel-unbin.com/golang_api/delimart_api/';

  Future<List<LaporanPenerimaanAll>?> fetchFromServer() async {
    var url = "$baseUrl/laporan_penerimaan_all";
    var response = await http.get(Uri.parse(url));

    if (response.statusCode == 200) {
      var body = convert.jsonDecode(response.body);
      if (body == null || body.isEmpty) {
        return null;
      }
      List<LaporanPenerimaanAll> LaporanPenerimaanAllList = [];
      for (final item in body) {
        LaporanPenerimaanAllList.add(LaporanPenerimaanAll.fromJson(item));
      }
      return LaporanPenerimaanAllList;
    } else {
      throw Exception('Failed to load LaporanPenerimaanAll');
    }
  }
}

// Main Application
void main() {
  runApp(LaporanPenerimaanAllPage());
}

class LaporanPenerimaanAllPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Delimart App',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: LaporanPenerimaanAllList(),
    );
  }
}

class LaporanPenerimaanAllList extends StatefulWidget {
  @override
  _LaporanPenerimaanAllListState createState() =>
      _LaporanPenerimaanAllListState();
}

class _LaporanPenerimaanAllListState extends State<LaporanPenerimaanAllList> {
  late Future<List<LaporanPenerimaanAll>?> futureLaporanPenerimaanAll;

  @override
  void initState() {
    super.initState();
    futureLaporanPenerimaanAll = ApiService().fetchFromServer();
  }

  Future<void> _refreshLaporan() async {
    setState(() {
      futureLaporanPenerimaanAll = ApiService().fetchFromServer();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: Text(
          "Laporan Penerimaan Selama Ini",
          style: TextStyle(
            color: Colors.white,
            fontWeight: FontWeight.bold,
            fontSize: 15.0,
          ),
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
        onRefresh: _refreshLaporan,
        color: Color.fromARGB(255, 2, 10, 74),
        height: 200,
        backgroundColor: Colors.white,
        animSpeedFactor: 1,
        showChildOpacityTransition: false,
        child:FutureBuilder<List<LaporanPenerimaanAll>?>
      (
        future: futureLaporanPenerimaanAll,
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
                    Text('Laporan Penerimaan Selama ini kosong'),
                    SizedBox(height: 20),
                    ElevatedButton(
                      onPressed: _refreshLaporan,
                      child: Text('Refresh'),
                    ),
                  ],
                ),
              );
          } else {
            return ListView.builder(
              itemCount: snapshot.data!.length,
              itemBuilder: (context, index) {
                final LaporanPenerimaanAll laporan = snapshot.data![index];
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
                        Text('Pengeluaran: ${laporan.pengeluaran}'),
                        Text('Total Transaksi: ${laporan.transaksi}'),
                     ],
                      ),
                    ),
                  );
                },
              );
            }
          },
        ),
      ),
    );
  }
}
