<?php

namespace App\Http\Controllers;

use App\Services\PegawaiService;
use Illuminate\Http\Request;
use Illuminate\Pagination\LengthAwarePaginator;

class PegawaiController extends Controller
{
    protected $pegawaiService;

    public function __construct(PegawaiService $pegawaiService)
    {
        $this->pegawaiService = $pegawaiService;
    }

    public function index()
    {
        $pegawai = $this->pegawaiService->getPegawai();
        $perPage = 5;
        $currentPage = LengthAwarePaginator::resolveCurrentPage();
        $currentItems = array_slice($pegawai, ($currentPage - 1) * $perPage, $perPage);
        $paginatedItems = new LengthAwarePaginator($currentItems, count($pegawai), $perPage, $currentPage, [
            'path' => LengthAwarePaginator::resolveCurrentPath()
        ]);

        return view('pegawai.index', ['pegawai' => $paginatedItems]);
    }

    public function tambah()
    {
        $role = $this->pegawaiService->getAllRole();
        return view('pegawai.formSimpan', ['role' => $role]);
    }

    public function simpan(Request $request)
    {
        $data = [
            'kd_pegawai' => $request->kd_pegawai,
            'nama_pegawai' => $request->nama_pegawai,
            'tanggal_lahir' => $request->tanggal_lahir,
            'jenis_kelamin' => $request->jenis_kelamin,
            'alamat' => $request->alamat,
            'telepon' => $request->telepon,
            'kd_role' => $request->kd_role,
        ];
        $this->pegawaiService->createPegawai($data);
        return redirect()->route('pegawai');
    }

    public function edit($kd_pegawai)
    {
        $pegawai = $this->pegawaiService->getPegawaiById($kd_pegawai);
        $role = $this->pegawaiService->getAllRole();
        return view('pegawai.formUpdate', ['pegawai' => $pegawai, 'role' => $role]);
    }

    public function update($kd_pegawai, Request $request)
    {
        $data = [
            'nama_pegawai' => $request->nama_pegawai,
            'tanggal_lahir' => $request->tanggal_lahir,
            'jenis_kelamin' => $request->jenis_kelamin,
            'alamat' => $request->alamat,
            'telepon' => $request->telepon,
            'kd_role' => $request->kd_role,
        ];
        $this->pegawaiService->updatePegawai($kd_pegawai, $data);
        return redirect()->route('pegawai');
    }

    public function hapus($kd_pegawai)
    {
        $this->pegawaiService->deletePegawai($kd_pegawai);
        return redirect()->route('pegawai');
    }
}
