﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspapp.Models;
using aspapp.Models.VM;
using aspapp.Repositories;
using AutoMapper;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace aspapp.Services
{
    public class GuideService : IGuideService
    {
        private readonly IGuideRepository _guideRepository;
        private readonly IMapper _mapper;

        public GuideService(IGuideRepository guideRepository, IMapper mapper)
        {
            _guideRepository = guideRepository;
            _mapper = mapper;
        }

        // Pobieranie wszystkich przewodników jako GuideViewModel
        public async Task<List<GuideViewModel>> GetAllGuides()
        {
            var guides = await _guideRepository.GetAllGuides().ToListAsync(); // Ensure the data is materialized
            return guides.Select(guide => _mapper.Map<GuideViewModel>(guide)).ToList();
        }

        // Pobieranie przewodnika po ID, zwracanie jako GuideViewModel
        public async Task<GuideViewModel> GetGuideById(int guideId)
        {
            var guide = await _guideRepository.GetGuideById(guideId);
            if (guide == null)
            {
                throw new KeyNotFoundException($"Guide with Id {guideId} not found.");
            }

            return _mapper.Map<GuideViewModel>(guide);
        }

        // Dodawanie przewodnika na podstawie GuideViewModel
        public async Task AddGuide(GuideViewModel guideViewModel)
        {
            // Validate fields
            ValidateGuideViewModel(guideViewModel);

            // Check for valid email format
            if (!IsValidEmail(guideViewModel.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            var guide = _mapper.Map<Guide>(guideViewModel);
            await _guideRepository.AddGuide(guide);
        }

        // Aktualizacja przewodnika
        public async Task UpdateGuide(GuideViewModel guideViewModel)
        {
            // Validate fields
            ValidateGuideViewModel(guideViewModel);

            // Check for valid email format
            if (!IsValidEmail(guideViewModel.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            var guide = _mapper.Map<Guide>(guideViewModel);

            var existingGuide = await _guideRepository.GetGuideById(guide.GuideId);
            if (existingGuide == null)
            {
                throw new KeyNotFoundException($"Guide with Id {guide.GuideId} not found.");
            }

            await _guideRepository.UpdateGuide(guide);
        }

        // Usuwanie przewodnika
        public async Task DeleteGuide(int id)
        {
            var guide = await _guideRepository.GetGuideById(id);
            if (guide == null)
            {
                throw new KeyNotFoundException($"Guide with Id {id} not found.");
            }

            // You can add logic here to handle associated trips if necessary

            await _guideRepository.DeleteGuide(id);
        }

        // Helper method to validate GuideViewModel
        private void ValidateGuideViewModel(GuideViewModel guideViewModel)
        {
            if (string.IsNullOrEmpty(guideViewModel.Firstname) ||
                string.IsNullOrEmpty(guideViewModel.Lastname) ||
                string.IsNullOrEmpty(guideViewModel.Email))
            {
                throw new ArgumentException("All fields are required.");
            }
        }

        // Helper method to validate email format
        private bool IsValidEmail(string email)
        {
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }
    }
}
