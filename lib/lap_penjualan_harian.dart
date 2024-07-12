import 'package:flutter/material.dart';
import 'dart:convert' as convert;
import 'package:http/http.dart' as http;
import 'package:intl/intl.dart'; // Import package intl untuk formatting tanggal
import 'package:flutter_application_1/laporan.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';

class LaporanPenjualanHarian {
  final String tanggal;
  final int keuntungan;
  final int pendapatan;
  final String jam;
  final String struk;

  LaporanPenjualanHarian({
    required this.tanggal,
    required this.keuntungan,
    required this.pendapatan,
    required this.jam,
    required this.struk,
  });

  factory LaporanPenjualanHarian.fromJson(Map<String, dynamic> json) {
     // Mengubah format tanggal
    DateTime parsedDate = DateTime.parse(json['tanggal']);
    String formattedDate = DateFormat('MMM d, yyyy').format(parsedDate);
    return LaporanPenjualanHarian(
      tanggal: formattedDate,
      keuntungan: int.parse(json['keuntungan']),
      pendapatan: int.parse(json['pendapatan']),
      jam: json['jam'],
      struk: json['struk'],
    );
  }
}

class ApiService {
  final String baseUrl = 'https://praktikum-cpanel-unbin.com/golang_api/delimart_api/';

  Future<List<LaporanPenjualanHarian>?> fetchFromServer() async {
    var url = "$baseUrl/laporan_penjualan_harian";
    var response = await http.get(Uri.parse(url));

    if (response.statusCode == 200) {
      var body = convert.jsonDecode(response.body);
      if (body == null || body.isEmpty) {
        return null;
      }
      List<LaporanPenjualanHarian> LaporanPenjualanHarianList = [];
      for (final item in body) {
        LaporanPenjualanHarianList.add(LaporanPenjualanHarian.fromJson(item));
      }
      return LaporanPenjualanHarianList;
    } else {
      throw Exception('Failed to load LaporanPenjualanHarian');
    }
  }
}

// Main Application
void main() {
  runApp(LaporanPenjualanHarianPage());
}

class LaporanPenjualanHarianPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Delimart App',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: LaporanPenjualanHarianList(),
    );
  }
}

class LaporanPenjualanHarianList extends StatefulWidget {
  @override
  _LaporanPenjualanHarianListState createState() => _LaporanPenjualanHarianListState();
}

class _LaporanPenjualanHarianListState extends State<LaporanPenjualanHarianList> {
  late Future<List<LaporanPenjualanHarian>?> futureLaporanPenjualanHarian;

  @override
  void initState() {
    super.initState();
    futureLaporanPenjualanHarian = ApiService().fetchFromServer();
  }
Future<void> _laporan() async {
    setState(() {
      futureLaporanPenjualanHarian = ApiService().fetchFromServer();
    });
  }
  @override
  Widget build(BuildContext context) {
    String formattedDate = DateFormat('MMM d, yyyy').format(DateTime.now());
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: Text(
          "Laporan Penjualan Hari Ini - $formattedDate" ,
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
        color: Color.fromARGB(255, 2, 10, 74),
        height: 200,
        backgroundColor: Colors.white,
        animSpeedFactor: 1,
        showChildOpacityTransition: false,
        child: FutureBuilder<List<LaporanPenjualanHarian>?>(
        future: futureLaporanPenjualanHarian,
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
                    Text('Laporan Penjualan Hari ini kosong'),
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
                final LaporanPenjualanHarian laporan = snapshot.data![index];
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
                        Text('Jam Transaksi: ${laporan.jam}'),
                        Text('Pendapatan: ${laporan.pendapatan}'),
                        Text('Keuntungan: ${laporan.keuntungan}'),
                        Text('Struk: ${laporan.struk}'),
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
