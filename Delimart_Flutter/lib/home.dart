import 'package:flutter/material.dart';
import 'navbar.dart';
import 'dart:convert' as convert;
import 'package:http/http.dart' as http;

class HomePage extends StatefulWidget {
  const HomePage({Key? key}) : super(key: key);

  @override
  _HomePageState createState() => _HomePageState();
}

class LaporanArusStok {
  final int pendapatan;
  final int keuntungan;
  final int modal;
  final int transaksi;
  final int qty;
  final int rata2;
  final int jumlahPegawai;

  LaporanArusStok({
    required this.pendapatan,
    required this.keuntungan,
    required this.modal,
    required this.transaksi,
    required this.qty,
    required this.rata2,
    required this.jumlahPegawai,
  });
// ?? '0'
  factory LaporanArusStok.fromJson(Map<String, dynamic> json) {
    return LaporanArusStok(
      pendapatan: int.parse(json['total_pendapatan']),
      keuntungan: int.parse(json['total_keuntungan']),
      modal: int.parse(json['total_modal']),
      transaksi: int.parse(json['total_transaksi']),
      qty: int.parse(json['qty_terjual']),
      rata2: int.parse(json['rata2_penjualan']),
      jumlahPegawai: int.parse(json['jumlah_pegawai']),
    );
  }
}

class ApiService {
  final String baseUrl = 'https://praktikum-cpanel-unbin.com/golang_api/delimart_api/';

  Future<LaporanArusStok> fetchFromServer() async {
    var url = "$baseUrl/laporan_penjualan_home";
    var response = await http.get(Uri.parse(url));

    if (response.statusCode == 200) {
      var body = convert.jsonDecode(response.body);
      var data = body[0]; // Assuming the response is a list with a single item
      return LaporanArusStok.fromJson(data);
    } else {
      throw Exception('Failed to load LaporanArusStok');
    }
  }
}

class _HomePageState extends State<HomePage> {
  late Future<LaporanArusStok> futureData;

  @override
  void initState() {
    super.initState();
    futureData = ApiService().fetchFromServer();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: const Text(
          "Home Page",
          style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
        ),
        iconTheme: const IconThemeData(
          color: Colors.white, // Change your color here
        ),
      ),
      drawer: const NavBar(),
      body: FutureBuilder<LaporanArusStok>(
        future: futureData,
        builder: (context, snapshot) {
          if (snapshot.hasData) {
            var data = snapshot.data!;
            return Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: <Widget>[
                // Tambahkan Padding di sekitar Card pertama
                Padding(
                  padding: const EdgeInsets.all(20.0),
                  child: Card(
                    shape: RoundedRectangleBorder(
                      side: const BorderSide(
                        color: Color.fromARGB(255, 2, 10, 74),
                        width: 2,
                      ),
                      borderRadius: BorderRadius.circular(10),
                    ),
                    margin: const EdgeInsets.all(8),
                    child: InkWell(
                      onTap: () {
                        // Add your onTap function here
                      },
                      splashColor: Colors.blue,
                      child: Padding(
                        padding: const EdgeInsets.all(10.0),
                        child: Column(
                          mainAxisSize: MainAxisSize.min,
                          children: <Widget>[
                            ListTile(
                              title: Row(
                                mainAxisAlignment:
                                    MainAxisAlignment.spaceBetween,
                                children: <Widget>[
                                  Column(
                                    crossAxisAlignment:
                                        CrossAxisAlignment.start,
                                    children: <Widget>[
                                      Text(
                                        'Keuntungan',
                                        style: TextStyle(
                                            fontSize: 15.0,
                                            fontWeight: FontWeight.bold,
                                            color:
                                                Color.fromARGB(255, 2, 10, 74)),
                                      ),
                                      Text(
                                        'Rp.${data.keuntungan}',
                                        style: const TextStyle(fontSize: 15.0),
                                      ),
                                      Text(
                                        'Omset',
                                        style: TextStyle(
                                            fontSize: 15.0,
                                            fontWeight: FontWeight.bold,
                                            color:
                                                Color.fromARGB(255, 2, 10, 74)),
                                      ),
                                      Text(
                                        'Rp.${data.pendapatan}',
                                        style: const TextStyle(fontSize: 15.0),
                                      ),
                                    ],
                                  ),
                                  Column(
                                    crossAxisAlignment: CrossAxisAlignment.end,
                                    children: <Widget>[
                                      Text(
                                        'DELIMART',
                                        style: TextStyle(
                                            fontSize: 20.0,
                                            fontWeight: FontWeight.bold,
                                            color:
                                                Color.fromARGB(255, 2, 10, 74)),
                                      ),
                                      Text(
                                        'REKAP HARI INI',
                                        style: TextStyle(
                                            fontSize: 15.0,
                                            color:
                                                Color.fromARGB(255, 2, 10, 74)),
                                      ),
                                      Text(
                                        'Modal',
                                        style: TextStyle(
                                            fontSize: 15.0,
                                            fontWeight: FontWeight.bold,
                                            color:
                                                Color.fromARGB(255, 2, 10, 74)),
                                      ),
                                      Text(
                                        'Rp. ${data.modal}',
                                        style: const TextStyle(fontSize: 15.0),
                                      ),
                                    ],
                                  ),
                                ],
                              ),
                            ),
                            Row(
                              mainAxisAlignment: MainAxisAlignment.end,
                              children: <Widget>[
                                const SizedBox(width: 8),
                                IconButton(
                                  icon: const Icon(Icons.refresh,
                                      size: 30.0,
                                      color: Color.fromARGB(255, 2, 10, 74)),
                                  onPressed: () {
                                    setState(() {
                                      futureData =
                                          ApiService().fetchFromServer();
                                    });
                                  },
                                ),
                                const SizedBox(width: 8),
                              ],
                            ),
                          ],
                        ),
                      ),
                    ),
                  ),
                ),
                // GridView.count
                Expanded(
                  child: GridView.count(
                    padding: const EdgeInsets.all(20.0),
                    crossAxisCount: 2,
                    mainAxisSpacing: 10.0,
                    crossAxisSpacing: 10.0,
                    childAspectRatio: 1.5,
                    children: <Widget>[
                      // Card 1: Jumlah Transaksi
                      Card(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                            color: Color.fromARGB(255, 2, 10, 74),
                            width: 2,
                          ),
                          borderRadius: BorderRadius.circular(10),
                        ),
                        margin: const EdgeInsets.all(8),
                        child: InkWell(
                          onTap: () {
                            // Add your onTap function here
                          },
                          splashColor: Colors.blue,
                          child: Padding(
                            padding: const EdgeInsets.all(10.0),
                            child: Stack(
                              children: [
                                Column(
                                  mainAxisAlignment: MainAxisAlignment.start,
                                  crossAxisAlignment: CrossAxisAlignment.start,
                                  children: const <Widget>[
                                    Row(
                                      children: [
                                        Icon(Icons.swap_horiz,
                                            size: 25,
                                            color:
                                                Color.fromARGB(255, 2, 10, 74)),
                                        SizedBox(width: 5),
                                        Text("Jumlah Transaksi",
                                            style: TextStyle(
                                                fontSize: 12.0,
                                                fontWeight: FontWeight.bold)),
                                      ],
                                    ),
                                  ],
                                ),
                                Positioned(
                                  bottom: 8,
                                  right: 8,
                                  child: Text(
                                    '${data.transaksi}',
                                    style: const TextStyle(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold,
                                      color: Color.fromARGB(255, 2, 10, 74),
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                      ),
                      // Card 2: Qty Terjual
                      Card(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                            color: Color.fromARGB(255, 2, 10, 74),
                            width: 2,
                          ),
                          borderRadius: BorderRadius.circular(10),
                        ),
                        margin: const EdgeInsets.all(8),
                        child: InkWell(
                          onTap: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                  builder: (context) => const HomePage()),
                            );
                          },
                          splashColor: Colors.blue,
                          child: Padding(
                            padding: const EdgeInsets.all(10.0),
                            child: Stack(
                              children: [
                                Column(
                                  mainAxisAlignment: MainAxisAlignment.start,
                                  crossAxisAlignment: CrossAxisAlignment.start,
                                  children: const <Widget>[
                                    Row(
                                      children: [
                                        Icon(Icons.receipt_long,
                                            size: 25,
                                            color:
                                                Color.fromARGB(255, 2, 10, 74)),
                                        SizedBox(width: 5),
                                        Text("Qty Terjual",
                                            style: TextStyle(
                                                fontSize: 12.0,
                                                fontWeight: FontWeight.bold)),
                                      ],
                                    ),
                                  ],
                                ),
                                Positioned(
                                  bottom: 8,
                                  right: 8,
                                  child: Text(
                                    '${data.qty}',
                                    style: const TextStyle(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold,
                                      color: Color.fromARGB(255, 2, 10, 74),
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                      ),
                      // Card 3: Rata2 Penjualan
                      Card(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                            color: Color.fromARGB(255, 2, 10, 74),
                            width: 2,
                          ),
                          borderRadius: BorderRadius.circular(10),
                        ),
                        margin: const EdgeInsets.all(8),
                        child: InkWell(
                          onTap: () {
                            // Add your onTap function here
                          },
                          splashColor: Colors.blue,
                          child: Padding(
                            padding: const EdgeInsets.all(10.0),
                            child: Stack(
                              children: [
                                Column(
                                  mainAxisAlignment: MainAxisAlignment.start,
                                  crossAxisAlignment: CrossAxisAlignment.start,
                                  children: const <Widget>[
                                    Row(
                                      children: [
                                        Icon(Icons.shopping_cart,
                                            size: 25,
                                            color:
                                                Color.fromARGB(255, 2, 10, 74)),
                                        SizedBox(width: 5),
                                        Text("Rata2 Penjualan",
                                            style: TextStyle(
                                                fontSize: 12.0,
                                                fontWeight: FontWeight.bold)),
                                      ],
                                    ),
                                  ],
                                ),
                                Positioned(
                                  bottom: 8,
                                  right: 8,
                                  child: Text(
                                    '${data.rata2}',
                                    style: const TextStyle(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold,
                                      color: Color.fromARGB(255, 2, 10, 74),
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                      ),
                      // Card 4: Pegawai
                      Card(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                            color: Color.fromARGB(255, 2, 10, 74),
                            width: 2,
                          ),
                          borderRadius: BorderRadius.circular(10),
                        ),
                        margin: const EdgeInsets.all(8),
                        child: InkWell(
                          onTap: () {
                            // Add your onTap function here
                          },
                          splashColor: Colors.blue,
                          child: Padding(
                            padding: const EdgeInsets.all(10.0),
                            child: Stack(
                              children: [
                                Column(
                                  mainAxisAlignment: MainAxisAlignment.start,
                                  crossAxisAlignment: CrossAxisAlignment.start,
                                  children: const <Widget>[
                                    Row(
                                      children: [
                                        Icon(Icons.person,
                                            size: 25,
                                            color:
                                                Color.fromARGB(255, 2, 10, 74)),
                                        SizedBox(width: 5),
                                        Text("Pegawai",
                                            style: TextStyle(
                                                fontSize: 12.0,
                                                fontWeight: FontWeight.bold)),
                                      ],
                                    ),
                                  ],
                                ),
                                Positioned(
                                  bottom: 8,
                                  right: 8,
                                  child: Text(
                                    '${data.jumlahPegawai}',
                                    style: const TextStyle(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold,
                                      color: Color.fromARGB(255, 2, 10, 74),
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              ],
            );
          } else if (snapshot.hasError) {
            return Center(
              child: Text("${snapshot.error}"),
            );
          }
          return const Center(child: CircularProgressIndicator());
        },
      ),
    );
  }
}
