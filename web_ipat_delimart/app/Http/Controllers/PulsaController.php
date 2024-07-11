<?php

namespace App\Http\Controllers;

use App\Services\PulsaService;
use Illuminate\Http\Request;
use Illuminate\Pagination\LengthAwarePaginator;

class PulsaController extends Controller
{
    protected $pulsaService;

    public function __construct(PulsaService $pulsaService)
    {
        $this->pulsaService = $pulsaService;
    }

    public function index()
    {
        $pulsa = $this->pulsaService->getPulsa();
        $perPage = 5;
        $currentPage = LengthAwarePaginator::resolveCurrentPage();
        $currentItems = array_slice($pulsa, ($currentPage - 1) * $perPage, $perPage);
        $paginatedItems = new LengthAwarePaginator($currentItems, count($pulsa), $perPage, $currentPage, [
            'path' => LengthAwarePaginator::resolveCurrentPath()
        ]);

        return view('pulsa.index', ['pulsa' => $paginatedItems]);
    }

    public function tambah()
    {
        $provider = $this->pulsaService->getAllProvider();
        return view('pulsa.formSimpan', ['provider' => $provider]);
    }

    public function simpan(Request $request)
    {
        $data = [
            'kd_pulsa' => $request->kd_pulsa,
            'nama_produk_pulsa' => $request->nama_produk_pulsa,
            'kd_provider' => $request->kd_provider,
            'modal' => $request->modal,
            'harga' => $request->harga,
        ];
        $this->pulsaService->createPulsa($data);
        return redirect()->route('pulsa');
    }

    public function edit($kd_pulsa)
    {
        $pulsa = $this->pulsaService->getPulsaById($kd_pulsa);
        $provider = $this->pulsaService->getAllProvider();
        return view('pulsa.formUpdate', ['pulsa' => $pulsa, 'provider' => $provider]);
    }

    public function update($kd_pulsa, Request $request)
    {
        $data = [
            'nama_produk_pulsa' => $request->nama_produk_pulsa,
            'kd_provider' => $request->kd_provider,
            'modal' => $request->modal,
            'harga' => $request->harga,
        ];
        $this->pulsaService->updatePulsa($kd_pulsa, $data);
        return redirect()->route('pulsa');
    }

    public function hapus($kd_pulsa)
    {
        $this->pulsaService->deletePulsa($kd_pulsa);
        return redirect()->route('pulsa');
    }
}
