import 'package:flutter/material.dart';
import 'dart:convert' as convert;
import 'package:http/http.dart' as http;
import 'package:intl/intl.dart'; // Import package intl untuk formatting tanggal
import 'package:flutter_application_1/laporan.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';

class LaporanPenerimaanBulanan {
  final String tanggal;
  final int pengeluaran;
  final int totalTransaksi;

  LaporanPenerimaanBulanan({
    required this.tanggal,
    required this.pengeluaran,
    required this.totalTransaksi,
  });

  factory LaporanPenerimaanBulanan.fromJson(Map<String, dynamic> json) {
    DateTime parsedDate = DateTime.parse(json['tanggal']);
    String formattedDate = DateFormat('MMM d, yyyy').format(parsedDate);
    return LaporanPenerimaanBulanan(
      tanggal: formattedDate,
      pengeluaran: int.parse(json['pengeluaran']),
      totalTransaksi: int.parse(json['total_transaksi']),
    );
  }
}

class ApiService {
  final String baseUrl = 'https://praktikum-cpanel-unbin.com/golang_api/delimart_api/';

  Future<List<LaporanPenerimaanBulanan>?> fetchFromServer() async {
    var url = "$baseUrl/laporan_penerimaan_bulanan";
    var response = await http.get(Uri.parse(url));

    if (response.statusCode == 200) {
      var body = convert.jsonDecode(response.body);
      if (body == null || body.isEmpty) {
        return null;
      }
      List<LaporanPenerimaanBulanan> LaporanPenerimaanBulananList = [];
      for (final item in body) {
        LaporanPenerimaanBulananList.add(LaporanPenerimaanBulanan.fromJson(item));
      }
      return LaporanPenerimaanBulananList;
    } else {
      throw Exception('Failed to load LaporanPenerimaanBulanan');
    }
  }
}

// Main Application
void main() {
  runApp(LaporanPenerimaanBulananPage());
}

class LaporanPenerimaanBulananPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Delimart App',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: LaporanPenerimaanBulananList(),
    );
  }
}

class LaporanPenerimaanBulananList extends StatefulWidget {
  @override
  _LaporanPenerimaanBulananListState createState() => _LaporanPenerimaanBulananListState();
}

class _LaporanPenerimaanBulananListState extends State<LaporanPenerimaanBulananList> {
  late Future<List<LaporanPenerimaanBulanan>?> futureLaporanPenerimaanBulanan;

  @override
  void initState() {
    super.initState();
    futureLaporanPenerimaanBulanan = ApiService().fetchFromServer();
  }
Future<void> _laporan() async {
    setState(() {
      futureLaporanPenerimaanBulanan = ApiService().fetchFromServer();
    });
  }
  @override
  Widget build(BuildContext context) {
    String formattedMonth = DateFormat('MMM').format(DateTime.now());
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: Text(
          "Laporan Penerimaan Bulan Ini - $formattedMonth" ,
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
      body:LiquidPullToRefresh(
        onRefresh: _laporan,
        color:  Color.fromARGB(255, 2, 10, 74),
        height: 200,
        backgroundColor:  Colors.white,
        animSpeedFactor: 1,
        showChildOpacityTransition: false,
        child: FutureBuilder<List<LaporanPenerimaanBulanan>?>(
        future: futureLaporanPenerimaanBulanan,
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
                    Text('Laporan Penerimaan Bulan ini kosong'),
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
                final LaporanPenerimaanBulanan laporan = snapshot.data![index];
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
                      laporan.tanggal.toUpperCase(),
                      style: TextStyle(fontWeight: FontWeight.bold),
                    ),
                    subtitle: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text('Total Transaksi: ${laporan.totalTransaksi}'),
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
      )
    );
  }
}
