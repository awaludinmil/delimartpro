import 'package:flutter/material.dart';
import 'package:flutter_application_1/lap_penerimaan_all.dart';
import 'package:flutter_application_1/lap_penerimaan_bulanan.dart';
import 'package:flutter_application_1/lap_penerimaan_harian.dart';
import 'package:flutter_application_1/lap_penerimaan_tahunan.dart'; // Import your login page widget


class LaporanPenerimaanPage extends StatefulWidget {
  const LaporanPenerimaanPage({Key? key}) : super(key: key);

  @override
  _LaporanPenerimaanPageState createState() => _LaporanPenerimaanPageState();
}

class _LaporanPenerimaanPageState extends State<LaporanPenerimaanPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: const Text(
          "Laporan Penerimaan",
          style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
        ),
        iconTheme: const IconThemeData(
          color: Colors.white, // Change your color here
        ),
      ),
      
      body: ListView(
        padding: const EdgeInsets.all(25),
        children: <Widget>[
          _buildCard(Icons.trending_up, "Penerimaan Hari Ini", const Color.fromARGB(255, 2, 10, 74), () {
            // Navigate to login page
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanPenerimaanHarianPage()),
            );
          }),
          _buildCard(Icons.trending_up, "Penerimaan Bulan Ini", const Color.fromARGB(255, 2, 10, 74), () {
            // Navigate to login page
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanPenerimaanBulananPage()),
            );
          }),
          _buildCard(Icons.trending_up, "Penerimaan Tahun Ini", const Color.fromARGB(255, 2, 10, 74), () {
            // Navigate to login page
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanPenerimaanTahunanPage()),
            );
          }),
          _buildCard(Icons.trending_up, "Penerimaan Selama Ini", const Color.fromARGB(255, 2, 10, 74), () {
            // Navigate to login page
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanPenerimaanAllPage()),
            );
          }),
        ],
      ),
    );
  }

  Widget _buildCard(IconData icon, String title, Color iconColor, VoidCallback onTap) {
    return GestureDetector(
      onTap: onTap, // Call the provided onTap function
      child: Card(
        shape: RoundedRectangleBorder(
          side: const BorderSide(
            color: Color.fromARGB(255, 2, 10, 74),
            width: 2,
          ),
          borderRadius: BorderRadius.circular(10),
        ),
        margin: const EdgeInsets.all(8),
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Row(
            mainAxisSize: MainAxisSize.min,
            children: <Widget>[
              Icon(
                icon,
                size: 30,
                color: iconColor,
              ),
              SizedBox(width: 10),
              Text(title, style: TextStyle(fontSize: 15.0, fontWeight: FontWeight.bold)),
              Spacer(), // Optional: Adds space between title and arrow icon
              Icon(
                Icons.arrow_forward_ios,
                size: 20,
                color: const Color.fromARGB(255, 2, 10, 74),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
