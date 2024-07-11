import 'package:flutter/material.dart';
import 'dart:convert' as convert;
import 'package:http/http.dart' as http;
import 'laporan.dart'; // Import LaporanPage from laporan.dart
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';

class LaporanPenjualanBarang {
  final String namaBarang;
  final int keuntungan;
  final int pendapatan;
  final int qtyTerjual;

  LaporanPenjualanBarang({
    required this.namaBarang,
    required this.keuntungan,
    required this.pendapatan,
    required this.qtyTerjual,
  });

  factory LaporanPenjualanBarang.fromJson(Map<String, dynamic> json) {
    return LaporanPenjualanBarang(
      namaBarang: json['nama_barang'],
      keuntungan: int.parse(json['keuntungan']),
      pendapatan: int.parse(json['pendapatan']),
      qtyTerjual: int.parse(json['qty_terjual']),
    );
  }
}

class ApiService {
  final String baseUrl = 'https://praktikum-cpanel-unbin.com/golang_api/delimart_api/';

  Future<List<LaporanPenjualanBarang>?> fetchFromServer() async {
    var url = "$baseUrl/laporan_penjualan_barang";
    var response = await http.get(Uri.parse(url));

    if (response.statusCode == 200) {
      var body = convert.jsonDecode(response.body);
      if (body == null || body.isEmpty) {
        return null;
      }
      List<LaporanPenjualanBarang> LaporanPenjualanBarangList = [];
      for (final item in body) {
        LaporanPenjualanBarangList.add(LaporanPenjualanBarang.fromJson(item));
      }
      return LaporanPenjualanBarangList;
    } else {
      throw Exception('Failed to load LaporanPenjualanBarang');
    }
  }
}

// Main Application
void main() {
  runApp(LaporanPenjualanBarangPage());
}

class LaporanPenjualanBarangPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Delimart App',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: LaporanPenjualanBarangList(),
    );
  }
}

class LaporanPenjualanBarangList extends StatefulWidget {
  @override
  _LaporanPenjualanBarangListState createState() => _LaporanPenjualanBarangListState();
}

class _LaporanPenjualanBarangListState extends State<LaporanPenjualanBarangList> {
  late Future<List<LaporanPenjualanBarang>?> futureLaporanPenjualanBarang;

  @override
  void initState() {
    super.initState();
    futureLaporanPenjualanBarang = ApiService().fetchFromServer();
  }
Future<void> _laporan() async {
    setState(() {
      futureLaporanPenjualanBarang = ApiService().fetchFromServer();
    });
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: const Text(
          "Laporan Penjualan Barang",
          style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
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
        child:FutureBuilder<List<LaporanPenjualanBarang>?>(
        future: futureLaporanPenjualanBarang,
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
                    Text('Laporan Penjualan Barang Kosong'),
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
                final LaporanPenjualanBarang laporan = snapshot.data![index];
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
                      laporan.namaBarang.toUpperCase(),
                      style: TextStyle(fontWeight: FontWeight.bold),
                    ),
                    subtitle: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text('Keuntungan: ${laporan.keuntungan}'),
                        Text('Pendapatan: ${laporan.pendapatan}'),
                        Text('Qty Terjual: ${laporan.qtyTerjual}'),
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
