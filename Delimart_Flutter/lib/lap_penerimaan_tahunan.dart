import 'package:flutter/material.dart';
import 'dart:convert' as convert;
import 'package:http/http.dart' as http;
import 'package:intl/intl.dart'; // Import package intl untuk formatting bulan
import 'package:flutter_application_1/laporan.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';


class LaporanPenerimaanTahunan {
  final String bulan;
  final int pengeluaran;
  final String transaksi;

  LaporanPenerimaanTahunan({
    required this.bulan,
    required this.pengeluaran,
    required this.transaksi,
  });

  factory LaporanPenerimaanTahunan.fromJson(Map<String, dynamic> json) {
    String formattedMonth =
        DateFormat('MMM').format(DateTime(0, int.parse(json['bulan'])));
    return LaporanPenerimaanTahunan(
      bulan: formattedMonth,
      pengeluaran: int.parse(json['pengeluaran']),
      transaksi: json['total_transaksi'],
    );
  }
}

class ApiService {
  final String baseUrl = 'https://praktikum-cpanel-unbin.com/golang_api/delimart_api/';

  Future<List<LaporanPenerimaanTahunan>?> fetchFromServer() async {
    var url = "$baseUrl/laporan_penerimaan_tahunan";
    var response = await http.get(Uri.parse(url));

    if (response.statusCode == 200) {
      var body = convert.jsonDecode(response.body);
      if (body == null || body.isEmpty) {
        return null;
      }
      List<LaporanPenerimaanTahunan> LaporanPenerimaanTahunanList = [];
      for (final item in body) {
        LaporanPenerimaanTahunanList.add(
            LaporanPenerimaanTahunan.fromJson(item));
      }
      return LaporanPenerimaanTahunanList;
    } else {
      throw Exception('Failed to load LaporanPenerimaanTahunan');
    }
  }
}

// Main Application
void main() {
  runApp(LaporanPenerimaanTahunanPage());
}

class LaporanPenerimaanTahunanPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Delimart App',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: LaporanPenerimaanTahunanList(),
    );
  }
}

class LaporanPenerimaanTahunanList extends StatefulWidget {
  @override
  _LaporanPenerimaanTahunanListState createState() =>
      _LaporanPenerimaanTahunanListState();
}

class _LaporanPenerimaanTahunanListState
    extends State<LaporanPenerimaanTahunanList> {
  late Future<List<LaporanPenerimaanTahunan>?> futureLaporanPenerimaanTahunan;

  @override
  void initState() {
    super.initState();
    futureLaporanPenerimaanTahunan = ApiService().fetchFromServer();
  }

  Future<void> _refreshLaporan() async {
    setState(() {
      futureLaporanPenerimaanTahunan = ApiService().fetchFromServer();
    });
  }

  @override
  Widget build(BuildContext context) {
    String formattedDate = DateFormat('yyyy').format(DateTime.now());
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: Text(
          "Laporan Penerimaan Tahun Ini - $formattedDate",
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
      body:LiquidPullToRefresh(
        onRefresh: _refreshLaporan,
        color: Color.fromARGB(255, 2, 10, 74),
        height: 200,
        backgroundColor: Colors.white,
        animSpeedFactor: 1,
        showChildOpacityTransition: false,
        child:FutureBuilder<List<LaporanPenerimaanTahunan>?>(
        future: futureLaporanPenerimaanTahunan,
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
                    Text('Laporan Penerimaan Tahun ini kosong'),
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
                final LaporanPenerimaanTahunan laporan = snapshot.data![index];
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
                      laporan.bulan.toUpperCase(),
                      style: TextStyle(fontWeight: FontWeight.bold),
                    ),
                    subtitle: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text('Total Transaksi: ${laporan.transaksi}'),
                        Text('Pengeluaran: ${laporan.pengeluaran}'),
                      ],
                    ),
                  ),
                );
              },
            );
          }
        },
      ),
        ) ,
      
    );
  }
}
