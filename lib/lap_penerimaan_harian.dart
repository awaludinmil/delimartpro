import 'package:flutter/material.dart';
import 'dart:convert' as convert;
import 'package:http/http.dart' as http;
import 'package:intl/intl.dart';
import 'package:flutter_application_1/laporan.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';

class LaporanPenerimaanHarian {
  final String tanggal;
  final int pengeluaran;
  final String supplier;
  final String jam;
  final String nota;

  LaporanPenerimaanHarian({
    required this.tanggal,
    required this.pengeluaran,
    required this.supplier,
    required this.jam,
    required this.nota,
  });

  factory LaporanPenerimaanHarian.fromJson(Map<String, dynamic> json) {
    DateTime parsedDate = DateTime.parse(json['tanggal']);
    String formattedDate = DateFormat('MMM d, yyyy').format(parsedDate);
    return LaporanPenerimaanHarian(
      tanggal: formattedDate,
      pengeluaran: int.parse(json['pengeluaran']),
      supplier: json['supplier'],
      jam: json['jam'],
      nota: json['nota'],
    );
  }
}

class ApiService {
  final String baseUrl = 'https://praktikum-cpanel-unbin.com/golang_api/delimart_api/';

  Future<List<LaporanPenerimaanHarian>> fetchFromServer() async {
    var url = "$baseUrl/laporan_penerimaan_harian";
    var response = await http.get(Uri.parse(url));

    if (response.statusCode == 200) {
      var body = convert.jsonDecode(response.body);
      if (body == null || body.isEmpty) {
        return [];
      }
      List<LaporanPenerimaanHarian> LaporanPenerimaanHarianList = [];
      for (final item in body) {
        LaporanPenerimaanHarianList.add(LaporanPenerimaanHarian.fromJson(item));
      }
      return LaporanPenerimaanHarianList;
    } else {
      throw Exception('Failed to load LaporanPenerimaanHarian');
    }
  }
}

// Main Application
void main() {
  runApp(LaporanPenerimaanHarianPage());
}

class LaporanPenerimaanHarianPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Delimart App',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: LaporanPenerimaanHarianList(),
    );
  }
}

class LaporanPenerimaanHarianList extends StatefulWidget {
  @override
  _LaporanPenerimaanHarianListState createState() =>
      _LaporanPenerimaanHarianListState();
}

class _LaporanPenerimaanHarianListState
    extends State<LaporanPenerimaanHarianList> {
  late Future<List<LaporanPenerimaanHarian>> futureLaporanPenerimaanHarian;

  @override
  void initState() {
    super.initState();
    futureLaporanPenerimaanHarian = ApiService().fetchFromServer();
  }

  Future<void> _refreshLaporan() async {
    setState(() {
      futureLaporanPenerimaanHarian = ApiService().fetchFromServer();
    });
  }

  @override
  Widget build(BuildContext context) {
    String formattedDate = DateFormat('MMM d, yyyy').format(DateTime.now());
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: Text(
          "Laporan Penerimaan Hari Ini - $formattedDate",
          style: TextStyle(
            color: Colors.white,
            fontWeight: FontWeight.bold,
            fontSize: 15.0,
          ),
        ),
        iconTheme: const IconThemeData(
          color: Colors.white,
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
        child: FutureBuilder<List<LaporanPenerimaanHarian>>(
          future: futureLaporanPenerimaanHarian,
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
                    Text('Laporan Penerimaan Hari ini kosong'),
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
                  final LaporanPenerimaanHarian laporan = snapshot.data![index];
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
                          Text('Supplier: ${laporan.supplier}'),
                          Text('Pengeluaran: ${laporan.pengeluaran}'),
                          Text('Nota: ${laporan.nota}'),
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
